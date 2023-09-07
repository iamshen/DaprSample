using DaprTool.BuildingBlocks.EventBus.Events;

namespace DaprTool.BuildingBlocks.EventBus.Abstractions;

/// <summary>
///     事件处理器
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IntegrationEvent
{
    Task Handle(TEvent @event);
}

/// <summary>
///     事件处理器
/// </summary>
public interface IEventHandler
{
}