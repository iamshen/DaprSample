using DaprTool.BuildingBlocks.Abstractions.Events;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Domain.Core.Events.TradeOrder;

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