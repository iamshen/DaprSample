using DaprTool.BuildingBlocks.EventBus.Events;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Domain.Interfaces.Events.SaleOrder;

/// <summary>
///     订单已提交, 待支付事件
/// </summary>
public record OrderSubmittedToPayEvent : IntegrationEvent
{
    public string OrderNo { get; set; } = string.Empty;

    public string? Remark { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset UpdatedTime { get; set; }

    public IList<SaleOrderItem> OrderItems { get; set; } = new List<SaleOrderItem>();
}