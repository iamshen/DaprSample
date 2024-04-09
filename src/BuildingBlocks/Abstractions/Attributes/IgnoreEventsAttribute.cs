namespace DaprTool.BuildingBlocks.Domain.Attributes;

/// <summary>
/// 要忽略的类型特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
public sealed class IgnoreEventsAttribute : Attribute
{
    #region 属性

    /// <summary>
    /// 需要忽略的Event类型
    /// </summary>
    public IReadOnlyList<Type> EventTypes { get; }

    #endregion

    #region 初始化

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="eventTypes">要忽略的类型集合</param>
    public IgnoreEventsAttribute(params Type[] eventTypes) => EventTypes = eventTypes.ToList();

    #endregion
}