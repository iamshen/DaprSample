using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ordering.Infrastructure.Shared.Options;

/// <summary>
///     订单配置
/// </summary>
public class OrderingSettings
{
    /// <summary>
    ///    订单宽限期时间 （单位：分钟， 默认15分钟）
    /// </summary>
    public int GracePeriodTime { get; set; } = 15;
}

/// <summary>
///     订单配置扩展
/// </summary>
public static class OrderingSettingsExtensions
{
    public static OrderingSettings GetOrderingSettings(this IServiceProvider provider)
        => provider.GetRequiredService<IOptionsMonitor<OrderingSettings>>()?.CurrentValue ??
           new OrderingSettings();
}