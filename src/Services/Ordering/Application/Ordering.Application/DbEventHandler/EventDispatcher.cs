using Autofac;
using Autofac.Features.Indexed;
using DaprTool.BuildingBlocks.EventBus.Abstractions;
using DaprTool.BuildingBlocks.EventBus.Events;

namespace Ordering.Application.DbEventHandler;

/// <summary>
///     事件调度器
/// </summary>
public class EventDispatcher
{
    private IIndex<string, IDbEventHandler> _index;

    public EventDispatcher(IIndex<string, IDbEventHandler> index)
    {
        _index = index;
    }

    public async Task Dispatch<TEvent>(TEvent @event, Type handlerType) where TEvent : IntegrationEvent
    {
        dynamic handler = _index[handlerType.Name];

        if (handler != null)
            await handler.Handle((dynamic)@event);
        else
            throw new InvalidOperationException($"未找到事件类型的处理程序 {handlerType.Name}");
    }
}