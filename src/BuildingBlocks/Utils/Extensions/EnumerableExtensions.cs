#nullable disable

using System.Text;

namespace DaprTool.BuildingBlocks.Utils.Extensions;

#region IEnumerable 集合扩展方法

/// <summary>
///     IEnumerable 集合扩展方法
/// </summary>
public static class EnumerableExtensions
{
    #region 根据第三方条件是否为真来决定是否执行指定条件的查询

    /// <summary>
    ///     根据第三方条件是否为真来决定是否执行指定条件的查询
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要查询的数据源</param>
    /// <param name="predicate">查询条件</param>
    /// <param name="condition">第三方条件(bool 表达式)</param>
    /// <returns>返回查询的结果</returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        source = source as IList<T> ?? source.ToList();
        return condition ? source.Where(predicate) : source;
    }

    #endregion

    #region 将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号

    /// <summary>
    ///     将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号
    /// </summary>
    /// <param name="collection"> 要处理的集合 </param>
    /// <param name="separator"> 分隔符，默认为逗号 </param>
    /// <returns> 拼接后的字符串 </returns>
    public static string ExpandAndToString<T>(this IEnumerable<T> collection, string separator = ",")
    {
        return collection.ExpandAndToString(t => t?.ToString(), separator);
    }

    #endregion

    #region 循环集合的每一项，调用委托生成字符串，返回合并后的字符串。默认分隔符为逗号

    /// <summary>
    ///     循环集合的每一项，调用委托生成字符串，返回合并后的字符串。默认分隔符为逗号
    /// </summary>
    /// <param name="collection">待处理的集合</param>
    /// <param name="itemFormatFunc">单个集合项的转换委托</param>
    /// <param name="separator">分隔符，默认为逗号</param>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <returns></returns>
    public static string ExpandAndToString<T>(this IEnumerable<T> collection, Func<T, string> itemFormatFunc,
        string separator = ",")
    {
        collection = collection as IList<T> ?? collection.ToList();
        if (!collection.Any()) return string.Empty;
        var sb = new StringBuilder();
        var i = 0;
        var count = collection.Count();
        foreach (var t in collection)
        {
            if (i == count - 1)
                sb.Append(itemFormatFunc(t));
            else
                sb.Append(itemFormatFunc(t) + separator);
            i++;
        }

        return sb.ToString();
    }

    #endregion
}

#endregion