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
    /// <param name="showSize">每页要显示的记录数</param>
    public PagedList(int totoRecords, int showSize) : this()
    {
        TotalPages = (int)Math.Ceiling((double)totoRecords / (showSize <= 0 ? 1 : showSize));
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
    ///     当前实际的记录数
    /// </summary>
    public int PageSize => Data.Count;

    /// <summary>
    ///     第几页
    /// </summary>
    public int PageIndex { get; set; }

    #endregion
}

#endregion