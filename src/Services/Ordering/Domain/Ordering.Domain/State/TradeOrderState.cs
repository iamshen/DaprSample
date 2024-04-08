using Ordering.Infrastructure.Shared.Enumerations.TradeOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Domain.State;

/// <summary>
///     黄金买卖料订单
/// </summary>
public class TradeOrderState
{
    /// <summary>
    ///     订单号
    /// </summary>
    public string OrderNo { get; set; } = string.Empty;

    /// <summary>
    ///     订单状态
    /// </summary>
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;

    /// <summary>
    ///     支付状态
    /// </summary>
    public PayStatus PayStatus { get; set; } = PayStatus.Created;

    /// <summary>
    ///     交易状态
    /// </summary>
    public TradeStatus TradeStatus { get; set; } = TradeStatus.Created;

    /// <summary>
    ///     购买人
    /// </summary>
    public Buyer Buyer { get; set; } = new();

    /// <summary>
    ///     销售员工
    /// </summary>
    public Seller Seller { get; set; } = new();

    /// <summary>
    ///     销售门店
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
    public ICollection<TradeItem> OrderItems { get; set; } = new List<TradeItem>();
}