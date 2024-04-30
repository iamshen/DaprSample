using Microsoft.Extensions.DependencyInjection;

namespace WebAdmin.Shared.Infrastructure;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     为 Blazor 库添加 Web UI Web 组件所需的常用客户端服务
    /// </summary>
    /// <param name="services">Service collection</param>
    public static IServiceCollection AddAdminUiClientServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppVersionService, AppVersionService>();
        services.AddSingleton<CacheStorageAccessor>();
        services.AddHttpClient<IStaticAssetService, HttpBasedStaticAssetService>();
        services.AddSingleton<NavProvider>();

        return services;
    }

    /// <summary>
    ///     为 Blazor 库添加 Web UI 组件所需的通用服务器服务
    /// </summary>
    /// <param name="services">Service collection</param>
    public static IServiceCollection AddAdminUiServerServices(this IServiceCollection services)
    {
        services.AddScoped<IAppVersionService, AppVersionService>();
        services.AddScoped<CacheStorageAccessor>();
        services.AddHttpClient<IStaticAssetService, ServerStaticAssetService>();
        services.AddSingleton<NavProvider>();

        return services;
    }
}