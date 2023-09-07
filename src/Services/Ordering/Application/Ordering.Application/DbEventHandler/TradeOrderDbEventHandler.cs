using DaprTool.BuildingBlocks.EventBus.Abstractions;
using Ordering.Domain.Interfaces.Events.TradeOrder;

namespace Ordering.Application.DbEventHandler;

public class TradeOrderDbEventHandler : IEventHandler<OrderSubmittedDbEvent>
{
    public async Task Handle(OrderSubmittedDbEvent @event)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}