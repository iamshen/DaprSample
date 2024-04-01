using DaprTool.BuildingBlocks.Abstractions.Events;

namespace Ordering.Domain.Core.Events.TradeOrder;

/// <summary>
///     订单已经取消
/// </summary>
public class OrderStatusChangeToCancelEvent : IntegrationEvent
{
    public string OrderNo { get; set; } = string.Empty;
    public string? Remark { get; set; }
}