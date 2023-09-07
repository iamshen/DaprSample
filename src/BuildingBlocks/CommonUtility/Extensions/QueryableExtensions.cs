using System.ComponentModel;
using System.Linq.Expressions;
using DaprTool.BuildingBlocks.CommonUtility.ValueObjects;

namespace System.Linq;

/// <summary>
///     IQueryable 查询扩展
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    ///     根据第三方条件是否为真来决定是否执行指定条件的查询
    /// </summary>
    /// <typeparam name="T">动态类型 </typeparam>
    /// <param name="source">  要查询的源  </param>
    /// <param name="predicate"> 查询条件 </param>
    /// <param name="condition"> 第三方条件 </param>
    /// <returns> 查询的结果 </returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate,
        bool condition)
    {
        return condition
            ? source.Where(predicate)
            : source;
    }

    /// <summary>
    ///     分页查询 (请注意分页前，先排序)
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="source">    </param>
    /// <param name="pageIndex"> </param>
    /// <param name="pageSize">  </param>
    /// <returns> </returns>
    public static IQueryable<T> PageBy<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        var skipCount = (pageIndex - 1) * pageSize;
        if (skipCount < 0) skipCount = 0;
        return source.Skip(skipCount).Take(pageSize);
    }

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="source">      </param>
    /// <param name="pageRequest"> </param>
    /// <returns> </returns>
    public static IQueryable<T> PageBy<T>(this IQueryable<T> source, PageRequest pageRequest)
    {
        return source.PageBy(pageRequest.PageIndex, pageRequest.PageSize);
    }

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="pageRequest">分页请求对象</param>
    /// <param name="keySelector">排序字段</param>
    /// <param name="listSortDirection">指定集合的排序方式</param>
    /// <returns></returns>
    public static IQueryable<TSource> PageBy<TSource, TKey>(this IQueryable<TSource> source,
        PageRequest pageRequest,
        Expression<Func<TSource, TKey>> keySelector,
        ListSortDirection listSortDirection = ListSortDirection.Descending)
    {
        source = listSortDirection == ListSortDirection.Ascending
            ? source.OrderBy(keySelector)
            : source.OrderByDescending(keySelector);

        return source.PageBy(pageRequest);
    }

    /// <summary>
    ///     把<see cref="IQueryable{T}" />集合按指定字段与排序方式进行排序
    /// </summary>
    /// <param name="source">要排序的数据集</param>
    /// <param name="propertyName">排序属性名</param>
    /// <param name="sortDirection">排序方向</param>
    /// <typeparam name="T">动态类型</typeparam>
    /// <returns>排序后的数据集</returns>
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source,
        string propertyName,
        ListSortDirection sortDirection = ListSortDirection.Ascending)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

        return CollectionPropertySorter<T>.OrderBy(source, propertyName, sortDirection);
    }

    /// <summary>
    ///     把<see cref="IOrderedQueryable{T}" />集合继续按指定字段排序方式进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="propertyName">排序属性名</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns></returns>
    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source,
        string propertyName,
        ListSortDirection sortDirection = ListSortDirection.Ascending)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

        return CollectionPropertySorter<T>.ThenBy(source, propertyName, sortDirection);
    }
}