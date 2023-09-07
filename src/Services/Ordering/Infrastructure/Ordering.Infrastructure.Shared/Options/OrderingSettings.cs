namespace Ordering.Infrastructure.Shared.Options;

public class OrderingSettings
{
    /// <summary>
    ///    订单宽限期时间 （单位：分钟， 默认15分钟）
    /// </summary>
    public int GracePeriodTime { get; set; } = 15;
}
