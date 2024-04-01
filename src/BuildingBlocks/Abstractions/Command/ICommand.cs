namespace DaprTool.BuildingBlocks.Abstractions.Command;

/// <summary>
///     命令接口
/// </summary>
public interface ICommand
{
    /// <summary>
    ///     命令ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     命令名称
    /// </summary>
    public string CommandName { get; }

    /// <summary>
    ///     命令时间
    /// </summary>
    public DateTimeOffset CommandTime { get; }
}