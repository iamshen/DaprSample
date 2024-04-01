using DaprTool.BuildingBlocks.Abstractions.Events;

namespace DaprTool.BuildingBlocks.Abstractions.EventBus;

/// <summary>
///     事件总线接口
/// </summary>
public interface IEventBus
{
    /// <summary>
    ///     发布事件
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    Task RaiseAsync(params IntegrationEvent[] events);
}