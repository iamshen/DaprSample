using DaprTool.BuildingBlocks.EventBus.Abstractions;
using Ordering.Domain.Interfaces.Events.TradeOrder;

namespace Ordering.Application.DbEventHandler;

/// <summary>
/// 买卖料订单 db 事件处理器
/// </summary>
public class TradeOrderDbEventHandler : IDbEventHandler
{
    public string HandlerKeyed => nameof(TradeOrderDbEventHandler);

    public async Task Handle(OrderSubmittedDbEvent @event)
    {
        // TODO: use linq2db insert to database
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

}