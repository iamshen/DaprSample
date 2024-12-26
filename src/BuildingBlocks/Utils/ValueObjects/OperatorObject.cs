namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

#region 操作人对象

/// <summary>
///     操作人对象
/// </summary>
public record OperatorObject<T> where T : IEquatable<T>
{
    /// <summary>
    ///     操作人ID
    /// </summary>
    public required T OperatorId { get; init; }

    /// <summary>
    ///     操作人用户名
    /// </summary>
    public string? OperatorUserName { get; init; }

    /// <summary>
    ///     操作人真实姓名
    /// </summary>
    public string? OperatorRealName { get; init; }
}

#endregion