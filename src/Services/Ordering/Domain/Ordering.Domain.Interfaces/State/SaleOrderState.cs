using Ordering.Infrastructure.Shared.Enumerations.SaleOrder;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Domain.Interfaces.State;

/// <summary>
///     销售订单
/// </summary>
public class SaleOrderState
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
    public IList<SaleOrderItem> OrderItems { get; set; } = new List<SaleOrderItem>();
}