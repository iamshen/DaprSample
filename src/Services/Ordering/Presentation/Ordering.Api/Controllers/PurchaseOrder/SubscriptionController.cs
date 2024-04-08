using Dapr;
using DaprTool.BuildingBlocks.Utils.Constant;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Domain.Events.TradeOrder;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ordering.Api.Controllers.TradeOrder;

/// <summary>
/// 买料订单 事件订阅控制器
/// </summary>
[HiddenApi]
[ApiController]
public class SubscriptionController : Controller
{
    [HttpPost(nameof(OrderSubmittedEvent))]
    [Topic(DaprConstants.PubSubName, nameof(OrderSubmittedEvent))]
    public Task HandleAsync(OrderSubmittedEvent @event, [FromServices] IMediator dispatcher)
    {
        return dispatcher.Publish(@event);
    }

    [HttpPost(nameof(OrderStatusChangeToCancelEvent))]
    [Topic(DaprConstants.PubSubName, nameof(OrderStatusChangeToCancelEvent))]
    public Task HandleAsync(OrderStatusChangeToCancelEvent @event, [FromServices] IMediator dispatcher)
    {
        return dispatcher.Publish(@event);
    }
}