namespace DaprTool.BuildingBlocks.Domain.Attributes;

/// <summary>
/// 事件特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class EventAttribute : Attribute
{
    #region 属性

    /// <summary>
    /// 给事件起个名字，默认为事件类型的FullName(改名字用于找到该名字对应的事件类型，方便反序列化)
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 是否默认
    /// </summary>
    public bool Default { get; set; }

    #endregion
}