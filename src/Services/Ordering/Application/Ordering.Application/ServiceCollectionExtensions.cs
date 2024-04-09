
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Interfaces;
using Ordering.Application.Services;


public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAppApiServices(this IServiceCollection services)
    {
        services.AddTransient<IPurchaseOrderApiService, PurchaseOrderApiService>();

        return services;
    }
}