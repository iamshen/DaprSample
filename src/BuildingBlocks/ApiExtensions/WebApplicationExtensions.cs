using System.ComponentModel.DataAnnotations;
using DaprTool.BuildingBlocks.ApiExtensions.ApiResource.Dto;
using DaprTool.BuildingBlocks.Dependency;
using DaprTool.BuildingBlocks.Utils.Constant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Options;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationExtensions
{
    /// <summary>
    ///     注册 api 应用服务
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        // 注册 Serilog
        builder.AddAppSerilog(DaprTool.BuildingBlocks.Utils.Constant.Constants.Ordering.AppId);
        // 注册 Api 资源
        builder.AddAppApiResource();
        // 注册 Swagger 
        builder.AddAppSwagger();
        // 注册 Api 控制器
        builder.Services.AddControllers(options =>
        {
            // 注册过滤器
            // 数模模型检查过滤器
            // options.Filters.Add(new ModelStateFilter());
            // options.Filters.Add<ExceptionFilter>();
            // 返回结果处理过滤器 (改用 Middleware/ResultMiddleware)
            options.Filters.Add(new ResultFilter(AspNetCore.Mvc.AppConstants.ResponseJsonContentType));
        });
        // 注册 自动依赖注入
        builder.Services.AddAutoInject();
        // 注册 验证器
        builder.Services.AddValidators();
    }

    #region 私有方法

    #region Serilog

    /// <summary>
    ///     注册 Serilog 日志
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="appName"></param>
    private static void AddAppSerilog(this WebApplicationBuilder builder, string appName)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .Enrich.WithProperty("ApplicationName", appName)
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    #endregion

    #region HealthChecks

    /// <summary>
    ///     添加健康检查
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="appName"></param>
    private static void AddAppHealthChecks(this WebApplicationBuilder builder, string appName)
    {
        var sqlCheckName = appName + "db-check";

        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDapr()
            .AddNpgSql(
                builder.Configuration[$"ConnectionStrings:{appName}"]!,
                name: sqlCheckName,
                tags: new[] { sqlCheckName });
    }

    #endregion

    #region ApiResource

    /// <summary>
    ///     注册 API 信息 到 IdentityServer4 的API资源中
    /// </summary>
    /// <param name="builder"></param>
    private static void AddAppApiResource(this WebApplicationBuilder builder)
    {
        #region 验证配置

        var options = builder.Configuration.GetSection(nameof(ApiResourceOptions)).Get<ApiResourceOptions>() ??
                      new ApiResourceOptions();

        if (!options.AutoRegisterApiResource)
            return;

        builder.Services.AddSingleton(options);
        if (string.IsNullOrWhiteSpace(options.ResourceName))
            throw new ArgumentException(Errors.ResourceNameCanNotNull);

        if (string.IsNullOrWhiteSpace(options.IdentityServerApiDomain))
            throw new ArgumentException(Errors.DomainServerCanNotNull);

        #endregion

        // TODO: 从 服务提供者中使用 EndpointDataSource 替代  IActionDescriptorCollectionProvider
        //var dataSource = builder.Services.BuildServiceProvider().GetRequiredService<EndpointDataSource>();
        // 获取控制器提供者
        var descriptorCollectionProvider = builder.Services.BuildServiceProvider()
            .GetRequiredService<IActionDescriptorCollectionProvider>();

        // 获取所有 Controller 和 Actions 然后以控制器分组
        var groups = descriptorCollectionProvider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .Select(x => new Discovering
            {
                AreaName = x.ControllerTypeInfo.GetAttribute<ApiExplorerSettingsAttribute>(false)?.GroupName ??
                           string.Empty,
                ControllerName = x.ControllerName,
                ActionName = x.ActionName,
                DisplayName = x.MethodInfo.GetAttribute<DisplayAttribute>(false)?.Name,
                Description = x.MethodInfo.GetAttribute<DisplayAttribute>(false)?.Description,
                RouteTemplate = x.AttributeRouteInfo?.Template
            })
            .GroupBy(x => x.ControllerName)
            .ToList();

        // 注册 API 资源 到 IdentityServer4 中
        var apiResource = new ApiResource
        {
            Name = options.ResourceName,
            DisplayName = string.IsNullOrWhiteSpace(options.ResourceDisplayName)
                ? options.ResourceName
                : options.ResourceDisplayName,
            Description = options.Description
        };

        // 按照控制器分组后，注册同一个控制器的不同路由下的资源。
        foreach (var group in groups)
        {
            var scopes = new List<ApiResourceScope>();

            apiResource.ApiResourceScopes.AddIfNotExist(new ApiResourceScope
            {
                Name = apiResource.Name.ToLower(),
                DisplayName = string.IsNullOrWhiteSpace(options.ResourceDisplayName)
                    ? options.ResourceName
                    : options.ResourceDisplayName,
                Description = options.Description
            }, x => x.Name == apiResource.Name.ToLower());
            foreach (var item in group)
            {
                var area = string.IsNullOrWhiteSpace(item.AreaName) ? string.Empty : $"{item.AreaName}.";
                var scope =
                    $"{apiResource.Name.ToLower()}|{area}{item.ControllerName.ToLower()}.{item.ActionName.ToLower()}";

                scopes.AddIfNotExist(new ApiResourceScope
                {
                    Name = scope,
                    DisplayName = item.DisplayName,
                    Description = item.Description
                }, x => x.Name == scope);
            }

            apiResource.ApiResourceScopes.AddRange(scopes);
        }

        var urls = new List<string>
        {
            $"{options.IdentityServerApiDomain}/api/id4api/ApiResources/Synchronous"
        };

        foreach (var url in urls)
        {
            var client = new RestClient();
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(apiResource);
            _ = client.ExecuteAsync(request).Result;
        }
    }

    #endregion

    #region Errors

    private static class Errors
    {
        public const string ResourceNameCanNotNull = "自动注册API资源失败，资源名称不能为空";
        public const string DomainServerCanNotNull = "自动注册API资源失败，认证服务域名不能为空";
    }

    #endregion

    #endregion
}