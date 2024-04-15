using Dapr;
using DaprTool.BuildingBlocks.Utils.Constant;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Domain.Interfaces.Events.PurchaseOrder;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ordering.Api.Controllers.PurchaseOrder;

/// <summary>
///     买料订单 事件订阅控制器
/// </summary>
[HiddenApi]
[ApiController]
public class SubscriptionController(IPublisher dispatcher) : Controller
{
    [HttpPost(nameof(OrderSubmittedEvent))]
    [Topic(DaprConstants.PubSubName, nameof(OrderSubmittedEvent))]
    public Task HandleAsync(OrderSubmittedEvent @event) => dispatcher.Publish(@event);

    [HttpPost(nameof(OrderStatusChangeToCancelEvent))]
    [Topic(DaprConstants.PubSubName, nameof(OrderStatusChangeToCancelEvent))]
    public Task HandleAsync(OrderStatusChangeToCancelEvent @event) => dispatcher.Publish(@event);
}