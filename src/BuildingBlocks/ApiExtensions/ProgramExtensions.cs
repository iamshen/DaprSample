using System.ComponentModel.DataAnnotations;
using DaprTool.BuildingBlocks.ApiExtensions.ApiResource.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Options;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="appName"></param>
    public static void AddAppSerilog(this WebApplicationBuilder builder, string appName)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"];

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl!)
            .Enrich.WithProperty("ApplicationName", appName)
            .CreateLogger();

        builder.Host.UseSerilog();
    }
    
    /// <summary>
    ///     添加健康检查
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="sqlCheckName"></param>
    /// <param name="connectionStringName"></param>
    public static void AddAppHealthChecks(this WebApplicationBuilder builder, string sqlCheckName,
        string connectionStringName = "default")
    {
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDapr()
            .AddNpgSql(
                builder.Configuration[$"ConnectionStrings:{connectionStringName}"]!,
                name: sqlCheckName,
                tags: new[] { sqlCheckName });
    }


    /// <summary>
    ///     注册 API 信息 到 IdentityServer4 的API资源中
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppApiResource(this WebApplicationBuilder builder)
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

    private static class Errors
    {
        public const string ResourceNameCanNotNull = "自动注册API资源失败，资源名称不能为空";
        public const string DomainServerCanNotNull = "自动注册API资源失败，认证服务域名不能为空";
    }
}