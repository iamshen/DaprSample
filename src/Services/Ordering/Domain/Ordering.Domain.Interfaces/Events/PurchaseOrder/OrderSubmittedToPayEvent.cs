using DaprTool.BuildingBlocks.Domain.Events;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Domain.Interfaces.Events.PurchaseOrder;

/// <summary>
///     订单已提交, 待支付事件
/// </summary>
public class OrderSubmittedToPayEvent : IntegrationEvent
{
    public string OrderNo { get; set; } = string.Empty;

    public string? Remark { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset UpdatedTime { get; set; }

    public IList<TradeItem> OrderItems { get; set; } = new List<TradeItem>();
}