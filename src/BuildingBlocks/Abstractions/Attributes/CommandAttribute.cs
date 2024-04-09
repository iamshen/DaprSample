namespace DaprTool.BuildingBlocks.Domain.Attributes;

/// <summary>
///     标识命令的特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class CommandAttribute : Attribute
{
    public CommandAttribute()
    {
    }

    public CommandAttribute(string name, bool @default = false)
    {
        Name = name;
        Default = @default;
    }

    /// <summary>
    ///     命令名称，默认为命令类型的FullName
    /// </summary>
    public required string? Name { get; set; }

    /// <summary>
    ///     是否默认
    /// </summary>
    public bool Default { get; set; }
}