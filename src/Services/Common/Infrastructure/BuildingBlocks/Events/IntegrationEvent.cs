namespace DaprTool.BuildingBlocks.EventBus.Events;

/// <summary>
///     集成事件
/// </summary>
public record IntegrationEvent
{
    /// <summary>
    ///     ActorId
    /// </summary>
    public string ActorId { get; set; } = string.Empty;

    /// <summary>
    ///     事件Id
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString("N");

    /// <summary>
    ///     事件事件
    /// </summary>
    public DateTimeOffset EventTime { get; } = DateTimeOffset.Now;
}