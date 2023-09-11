using Autofac;
using DaprTool.BuildingBlocks.EventBus.Abstractions;
using DaprTool.BuildingBlocks.EventBus.Events;

namespace Ordering.Application.DbEventHandler;

/// <summary>
///     事件调度器
/// </summary>
public class EventDispatcher
{
    /// <summary>
    ///     Autofac 的容器服务提供者
    /// </summary>
    /// <remarks> 相当于 Microsoft 中的 IServiceProvider</remarks>
    private readonly IContainer _container;

    public EventDispatcher(IContainer container)
    {
        _container = container;
    }

    public async Task Dispatch<TEvent>(TEvent @event, Type handlerType) where TEvent : IntegrationEvent
    {
        dynamic handler = _container.ResolveKeyed<IDbEventHandler>(handlerType.Name);

        if (handler != null)
            await handler.Handle((dynamic)@event);
        else
            throw new InvalidOperationException($"未找到事件类型的处理程序 {handlerType.Name}");
    }
}