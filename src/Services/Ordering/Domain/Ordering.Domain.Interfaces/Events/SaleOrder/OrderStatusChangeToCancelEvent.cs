
using DaprTool.BuildingBlocks.EventBus.Events;

namespace Ordering.Domain.Interfaces.Events.SaleOrder;

/// <summary>
///     订单已经取消
/// </summary>
public record OrderStatusChangeToCancelEvent : IntegrationEvent
{
    public string OrderNo { get; set; } = string.Empty;
    public string? Remark { get; set; }
}
