
using DaprTool.BuildingBlocks.Utils.ValueObjects;


namespace DaprTool.BuildingBlocks.Utils.Attributes;


#region 标识分页查询如果发生异常则对结果做默认处理

/// <summary>
/// 标识分页查询如果发生异常则对结果做默认处理
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class PageRequestErrorDefaultResultAttribute : Attribute
{
    /// <summary>
    /// 要返回的默认的结果
    /// </summary>
    public object Result { get; init; } = new PagedList<object>();
}

#endregion