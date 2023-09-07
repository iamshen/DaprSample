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
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    /// <summary>
    ///     事件时间
    /// </summary>
    public long EventTime { get; set; }
}