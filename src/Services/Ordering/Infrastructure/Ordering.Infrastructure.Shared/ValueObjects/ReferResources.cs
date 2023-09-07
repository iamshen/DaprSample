namespace Ordering.Infrastructure.Shared.ValueObjects;

/// <summary>
///     引用资源
/// </summary>
public record ReferResources
{
    /// <summary>
    ///     URL链接
    /// </summary>
    public string Url { get; init; } = string.Empty;

    /// <summary>
    ///     资源描述
    /// </summary>
    public string? Description { get; init; }
}