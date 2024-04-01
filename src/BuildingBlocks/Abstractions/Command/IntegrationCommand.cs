using DaprTool.BuildingBlocks.Abstractions.Attributes;
using System.Reflection;

namespace DaprTool.BuildingBlocks.Abstractions.Command;

/// <summary>
/// 命令基类
/// </summary>
public abstract class IntegrationCommand : ICommand
{
    public IntegrationCommand()
    {
        var type = GetType();
        var attributes = type.GetCustomAttributes<CommandAttribute>();
        var commandAttributes = attributes as CommandAttribute[] ?? attributes.ToArray();
        if (!commandAttributes.Any())
        {
            CommandName = type!.FullName ?? "";
        }
        else
        {
            var defaultAttr = commandAttributes.LastOrDefault(m => m.Default);
            CommandName = defaultAttr == null ? commandAttributes.LastOrDefault()?.Name : defaultAttr.Name;
        }
    }

    /// <summary>
    ///     命令ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     命令名称
    /// </summary>
    public required string CommandName { get; init; } 

    /// <summary>
    ///     命令时间
    /// </summary>
    public DateTimeOffset CommandTime { get; } = TimeProvider.System.GetUtcNow();
}