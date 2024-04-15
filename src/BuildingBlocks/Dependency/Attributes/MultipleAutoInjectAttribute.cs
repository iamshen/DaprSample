namespace DaprTool.BuildingBlocks.Dependency;

#region 多重注入

/// <summary>
///     标记允许多重注入，即一个接口可以注入多个实例
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class MultipleAutoInjectAttribute : Attribute;

#endregion