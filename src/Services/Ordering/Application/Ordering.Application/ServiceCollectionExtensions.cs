
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
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddTransient<ITradeOrderService, TradeOrderService>();

        return services;
    }
}