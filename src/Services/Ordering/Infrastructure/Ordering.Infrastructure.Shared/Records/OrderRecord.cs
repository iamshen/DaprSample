using System.Runtime.Serialization;

namespace Ordering.Infrastructure.Shared.Records;

/// <summary>
///     订单id 单号 对象
/// </summary>
[DataContract]
public record OrderRecord
{

    public OrderRecord(string orderId, string orderNo)
    {
        OrderId = orderId;
        OrderNo = orderNo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [property: DataMember]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [property: DataMember]
    public string OrderNo { get; set; } = string.Empty;
}