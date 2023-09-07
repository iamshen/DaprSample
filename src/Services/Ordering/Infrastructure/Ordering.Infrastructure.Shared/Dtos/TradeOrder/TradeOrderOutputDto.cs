using Ordering.Infrastructure.Shared.Enumerations.TradeOrder;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Infrastructure.Shared.Dtos.TradeOrder;

public class TradeOrderOutputDto
{
    /// <summary>
    ///     订单Id
    /// </summary>
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    ///     订单号
    /// </summary>
    public string OrderNo { get; set; } = string.Empty;

    /// <summary>
    ///     订单状态
    /// </summary>
    public OrderStatus OrderStatus { get; set; }

    /// <summary>
    ///     订单状态说明
    /// </summary>
    public string OrderStatusDesc => OrderStatus.ToString();

    /// <summary>
    ///     支付状态
    /// </summary>
    public PayStatus PayStatus { get; set; }

    /// <summary>
    ///     支付状态说明
    /// </summary>
    public string PayStatusDesc => PayStatus.ToString();

    /// <summary>
    ///     交易状态
    /// </summary>
    public TradeStatus TradeStatus { get; set; }

    /// <summary>
    ///     交易状态说明
    /// </summary>
    public string TradeStatusDesc => TradeStatus.ToString();

    /// <summary>
    ///     购买人
    /// </summary>
    public Buyer Buyer { get; set; } = new();

    /// <summary>
    ///     销售员工
    /// </summary>
    public Seller Seller { get; set; } = new();

    /// <summary>
    ///     销售柜台
    /// </summary>
    public SaleStore SaleStore { get; set; } = new();

    /// <summary>
    ///     备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

    /// <summary>
    ///     更新时间
    /// </summary>
    public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.Now;

    /// <summary>
    ///     订单明细
    /// </summary>
    public IList<TradeItem> OrderItems { get; set; } = new List<TradeItem>();
}