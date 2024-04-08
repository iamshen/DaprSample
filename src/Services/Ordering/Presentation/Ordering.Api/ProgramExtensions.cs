using Dapr.Client;
using Dapr.Extensions.Configuration;
using DaprTool.BuildingBlocks.Abstractions.Actors;
using DaprTool.BuildingBlocks.Abstractions.EventBus;
using DaprTool.BuildingBlocks.Utils.Constant;
using FluentValidation;
using Ordering.Domain.Actors;
using Ordering.Domain.Commands.TradeOrder;
using Ordering.Domain.Observer.TradeOrder;
using Ordering.Infrastructure.Shared.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    /// <summary>
    ///     添加应用 Dapr
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppDapr(this WebApplicationBuilder builder)
    {
        // 注册 Dapr 客户端
        builder.Services.AddDaprClient();
        // 配置 Dapr 客户端
        var client = new DaprClientBuilder().Build();
        // 注册 Dapr Secret 配置
        builder.Configuration.AddDaprSecretStore(DaprConstants.SecretStore, client);
        // 注册 Dapr 配置
        builder.Configuration.AddDaprConfigurationStore(DaprConstants.ConfigurationStore, Array.Empty<string>(), client, TimeSpan.FromMinutes(30));
        // 注册 Dapr 事件总线
        builder.Services.AddScoped<IEventBus, DaprEventBus>();
        // 注册 Dapr Actor 
        builder.Services.AddActors(options =>
        {
            options.Actors.RegisterActor<NumberGeneratorActor>();
            options.Actors.RegisterActor<TradeOrderProcessActor>();
        });
    }
    /// <summary>
    ///     添加应用服务
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        // 注册 Serilog
        builder.AddAppSerilog(AppConstants.Ordering.AppId);
        // 注册 MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(TradeOrderDbEventHandler).Assembly));
        // 注册 订单 配置
        builder.Services.Configure<OrderingSettings>(builder.Configuration);
        // 注册 应用命令验证器
        builder.AddAppValidators();
        // 注册 Swagger 
        builder.AddAppSwagger();
        // 注册 Api 资源
        builder.AddAppApiResource();
        // 注册 Api 服务
        builder.Services.AddAppServices();
        // 注册 Api 控制器
        builder.Services.AddControllers();
        // 注册 健康检查
        builder.AddAppHealthChecks("ordering-db-check", "Ordering");
        // 注册 业务数据库
        builder.Services.AddAppDataConnection(builder.Configuration.GetConnectionString("Ordering")!);
    }

    /// <summary>
    ///     添加 应用命令验证器
    /// </summary>
    /// <param name="builder"></param>
    private static void AddAppValidators(this WebApplicationBuilder builder)
    {
        // Add  Validators 
        builder.Services.AddValidatorsFromAssemblyContaining<CreateTradeOrderCommandValidator>(
            ServiceLifetime.Transient);
    }
}