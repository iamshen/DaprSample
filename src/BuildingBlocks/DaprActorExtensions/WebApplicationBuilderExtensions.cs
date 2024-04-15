using Dapr.Actors.Runtime;
using Dapr.Client;
using Dapr.Extensions.Configuration;
using DaprTool.BuildingBlocks.Domain.EventBus;
using DaprTool.BuildingBlocks.Utils.Constant;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     WebApplication Builder Extensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     添加应用 Dapr
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    public static void RegisterAppDapr(this WebApplicationBuilder builder, Action<ActorRuntimeOptions>? configure)
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
        if (configure != null)
            builder.Services.AddActors(configure);
    }
}