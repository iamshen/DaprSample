using Ordering.Infrastructure.Shared.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    /// <summary>
    ///     添加应用服务
    /// </summary>
    /// <param name="builder"></param>
    public static void RegisterAppServices(this WebApplicationBuilder builder)
    {
        // 注册 MediatR
        builder.Services.AddAppMediators();
        // 注册 应用服务
        builder.AddAppServices();
        // 注册 应用配置
        builder.Services.Configure<OrderingSettings>(builder.Configuration);
        // 注册 业务数据库
        builder.Services.AddOrderAppDataConnection(builder.Configuration);
    }
}