using MediatR;

namespace DaprTool.BuildingBlocks.Domain.Events;

/// <summary>
///     事件基类
/// </summary>
public abstract class IntegrationEvent : IEvent, INotification
{
    /// <summary>
    ///     事件ID
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     事件时间
    /// </summary>
    public DateTimeOffset EventTime { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    ///     ActorId
    /// </summary>
    public string ActorId { get; set; } = string.Empty;
}