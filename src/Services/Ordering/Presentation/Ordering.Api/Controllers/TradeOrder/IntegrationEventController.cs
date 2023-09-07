using Dapr;
using DaprTool.BuildingBlocks.CommonUtility.Constant;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.DbEventHandler;
using Ordering.Domain.Interfaces.Events.TradeOrder;

namespace Ordering.Api.Controllers.TradeOrder;

[ControllerName("TradeOrder")]
public class IntegrationEventController : ApiBaseController
{
    [HttpPost("OrderSubmitted")]
    [Topic(DaprConstants.PubSubName, nameof(OrderSubmittedDbEvent))]
    public Task HandleAsync(OrderSubmittedDbEvent @event, [FromServices] TradeOrderDbEventHandler handler)
    {
        return handler.Handle(@event);
    }
}