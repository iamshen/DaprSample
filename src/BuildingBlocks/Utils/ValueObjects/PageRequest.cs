using System.Linq.Expressions;

namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

#region 分页请求对象

/// <summary>
///     分页请求对象
/// </summary>
public class PageRequest
{
    #region 方法

    /// <summary>
    ///     获取默认的查询表达式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> GetDefaultPredicate<T>(bool defaultValue = true)
    {
        return m => defaultValue;
    }

    #endregion

    #region 成员变量

    /// <summary>
    ///     页索引
    /// </summary>
    private int _pageIndex = 1;

    /// <summary>
    ///     页大小
    /// </summary>
    private int _pageSize = 15;

    #endregion

    #region 属性

    /// <summary>
    ///     获取或设置 页索引
    /// </summary>
    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value <= 0 ? 1 : value;
    }

    /// <summary>
    ///     获取或设置 页大小
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            <= 0 => DefaultPageSize,
            > 100 => 100,
            _ => value
        };
    }

    /// <summary>
    ///     默认每页显示大记录数
    /// </summary>
    public static int DefaultPageSize => 50;

    /// <summary>
    ///     默认的页码
    /// </summary>
    public static int DefaultPageIndex => 1;

    /// <summary>
    ///     获取或设置 查询关键字
    /// </summary>
    public string? KeyWord { get; set; }

    #endregion
}

#endregion