using Dapr;
using DaprTool.BuildingBlocks.CommonUtility.Constant;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.DbEventHandler;
using Ordering.Domain.Interfaces.Events.TradeOrder;

namespace Ordering.Api.Controllers.TradeOrder;

/// <summary>
/// 买卖料订单 事件订阅控制器
/// </summary>
[ControllerName("TradeOrder")]
public class IntegrationEventController : ApiBaseController
{
    [HttpPost("OrderSubmitted")]
    [Topic(DaprConstants.PubSubName, nameof(OrderSubmittedDbEvent))]
    public Task HandleAsync(
        OrderSubmittedDbEvent @event,
        [FromServices] EventDispatcher dispatcher)
    {
        return dispatcher.Dispatch(@event, typeof(TradeOrderDbEventHandler));
    }
}