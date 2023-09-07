#nullable disable

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq.Expressions;

namespace System.Linq;

/// <summary>
///     集合类型字符串排序操作类
/// </summary>
/// <typeparam name="T">集合项类型</typeparam>
public static class CollectionPropertySorter<T>
{
    private static readonly ConcurrentDictionary<string, LambdaExpression> Cache = new();

    /// <summary>
    ///     按指定的属性名称对<see cref="IEnumerable{T}" />序列进行排序
    /// </summary>
    /// <param name="source"><see cref="IEnumerable{T}" />序列</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="sortDirection">排序方向</param>
    public static IOrderedEnumerable<T> OrderBy(IEnumerable<T> source, string propertyName,
        ListSortDirection sortDirection)
    {
        if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));
        dynamic expression = GetKeySelector(propertyName);
        var keySelector = expression.Compile();
        return sortDirection == ListSortDirection.Ascending
            ? Enumerable.OrderBy(source, keySelector)
            : Enumerable.OrderByDescending(source, keySelector);
    }

    /// <summary>
    ///     按指定的属性名称对<see cref="IOrderedEnumerable{T}" />进行继续排序
    /// </summary>
    /// <param name="source"><see cref="IOrderedEnumerable{T}" />序列</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="sortDirection">排序方向</param>
    public static IOrderedEnumerable<T> ThenBy(IOrderedEnumerable<T> source, string propertyName,
        ListSortDirection sortDirection)
    {
        if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));
        dynamic expression = GetKeySelector(propertyName);
        var keySelector = expression.Compile();
        return sortDirection == ListSortDirection.Ascending
            ? Enumerable.ThenBy(source, keySelector)
            : Enumerable.ThenByDescending(source, keySelector);
    }

    /// <summary>
    ///     按指定的属性名称对<see cref="IQueryable{T}" />序列进行排序
    /// </summary>
    /// <param name="source">IQueryable{T}序列</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns></returns>
    public static IOrderedQueryable<T> OrderBy(IQueryable<T> source, string propertyName,
        ListSortDirection sortDirection)
    {
        if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));
        dynamic keySelector = GetKeySelector(propertyName);
        return sortDirection == ListSortDirection.Ascending
            ? Queryable.OrderBy(source, keySelector)
            : Queryable.OrderByDescending(source, keySelector);
    }

    /// <summary>
    ///     按指定的属性名称对<see cref="IOrderedQueryable{T}" />序列进行排序
    /// </summary>
    /// <param name="source">IOrderedQueryable{T}序列</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns></returns>
    public static IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> source, string propertyName,
        ListSortDirection sortDirection)
    {
        if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));
        dynamic keySelector = GetKeySelector(propertyName);
        return sortDirection == ListSortDirection.Ascending
            ? Queryable.ThenBy(source, keySelector)
            : Queryable.ThenByDescending(source, keySelector);
    }

    private static LambdaExpression GetKeySelector(string keyName)
    {
        var type = typeof(T);
        var key = type.FullName + "." + keyName;
        if (Cache.ContainsKey(key)) return Cache[key];
        var param = Expression.Parameter(type);
        var propertyNames = keyName.Split('.');
        Expression propertyAccess = param;
        foreach (var propertyName in propertyNames)
        {
            var property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentNullException(string.Format("指定对象中不存在名称为“{0}”的属性。", propertyName));
            type = property.PropertyType;
            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
        }

        var keySelector = Expression.Lambda(propertyAccess, param);
        Cache[key] = keySelector;
        return keySelector;
    }
}