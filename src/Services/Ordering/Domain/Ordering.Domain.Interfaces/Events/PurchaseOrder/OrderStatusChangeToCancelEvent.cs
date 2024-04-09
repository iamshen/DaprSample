using DaprTool.BuildingBlocks.Domain.Events;

namespace Ordering.Domain.Interfaces.Events.PurchaseOrder;

/// <summary>
///     订单已经取消
/// </summary>
public class OrderStatusChangeToCancelEvent : IntegrationEvent
{
    public string OrderNo { get; set; } = string.Empty;
    public string? Remark { get; set; }
}