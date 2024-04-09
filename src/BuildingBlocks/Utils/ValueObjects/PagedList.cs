namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

#region 分页查询结果对象

/// <summary>
///     分页查询结果对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedList<T> where T : class
{
    #region 初始化

    /// <summary>
    ///     分页查询结果对象
    /// </summary>
    public PagedList()
    {
        Data = new List<T>();
        PageIndex = 1;
    }

    /// <summary>
    ///     分页查询结果对象(目的是为了自动计算TotalCount)
    /// </summary>
    /// <param name="totoRecords">总记录数</param>
    public PagedList(int totoRecords) : this()
    {
        TotalPages = (int)Math.Ceiling((double)totoRecords / (PageSize <= 0 ? 1 : PageSize));
        TotalCount = totoRecords;
    }

    #endregion

    #region 属性

    /// <summary>
    ///     结果集
    /// </summary>
    public List<T> Data { get; init; }

    /// <summary>
    ///     总的记录数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    ///     总页数
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    ///     页数
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    ///     第几页
    /// </summary>
    public int PageIndex { get; set; }

    #endregion
}

#endregion