namespace Ordering.Infrastructure.Shared.ValueObjects;

/// <summary>
///     订单id 单号 对象
/// </summary>
public class OrderRecord
{
    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="orderNo"></param>
    public OrderRecord(string orderId, string orderNo)
    {
        OrderId = orderId;
        OrderNo = orderNo;
    }

    /// <summary>
    ///     ctor
    /// </summary>
    public OrderRecord()
    {
        OrderId = string.Empty;
        OrderNo = string.Empty;
    }

    public string OrderId { get; set; }
    public string OrderNo { get; set; }
}