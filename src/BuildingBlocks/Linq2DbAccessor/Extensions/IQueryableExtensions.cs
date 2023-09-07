#nullable disable

using System.Linq.Expressions;
using System.Reflection;
using LinqToDB.Extensions;
using LinqToDB.Linq;

namespace System.Linq;

#region ITable的扩展方法

/// <summary>
///     ITable的扩展方法
/// </summary>
public static class IQueryableExtensions
{
    #region 私有变量

    private static MethodInfo _where;
    private static MethodInfo _left;
    private static MethodInfo _right;

    #endregion

    #region Json查询相关

    /// <summary>
    ///     构建JSON WHERE表达式
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IQueryable<TSource> JsonWhere<TSource>(this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        if (_where == null)
            _where = typeof(Queryable).GetMethods()
                .Where(w => w.IsGenericMethod && w.Name == nameof(Queryable.Where))
                .First();

        Query.ClearCaches();

        var expression = ConvertExpression(predicate);

        return source.Provider.CreateQuery<TSource>(
            Expression.Call(null, _where.MakeGenericMethod(typeof(TSource)),
                new[] { source.Expression, Expression.Quote(expression) }));
    }

    /// <summary>
    ///     构建JSON WHERE IF表达式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static IQueryable<T> JsonWhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate,
        bool condition)
    {
        return condition
            ? source.JsonWhere(predicate)
            : source;
    }

    /// <summary>
    ///     构建LEFT表达式
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private static MethodCallExpression GetLeftExpression(Expression expression, Type type)
    {
        if (_left == null) _left = typeof(JsonWhereBuilder).GetMethod(nameof(JsonWhereBuilder.Left));
        var argu = Expression.Convert(expression, typeof(object));
        return Expression.Call(null, _left.MakeGenericMethod(type), argu);
    }

    /// <summary>
    ///     构建RIGHT表达式
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private static MethodCallExpression GetRightExpression(Expression expression, Type type)
    {
        if (_right == null) _right = typeof(JsonWhereBuilder).GetMethod(nameof(JsonWhereBuilder.Right));
        var argu = Expression.Convert(expression, typeof(object));
        return Expression.Call(null, _right.MakeGenericMethod(type), argu);
    }

    /// <summary>
    ///     转换为SQL表达式
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    private static LambdaExpression ConvertExpression(LambdaExpression expression)
    {
        if (expression.Body is not BinaryExpression binaryExpression)
            throw new NotSupportedException("请检查表达式,当前表达式不为BinaryExpression");
        var type = GetFieldType(binaryExpression.Left);
        var left = GetLeftExpression(binaryExpression.Left, type);
        var right = GetRightExpression(binaryExpression.Right, type);
        var body = Expression.MakeBinary(binaryExpression.NodeType, left, right);
        return Expression.Lambda(body, expression.Parameters);
    }

    /// <summary>
    ///     获取字段类型
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    private static Type GetFieldType(Expression expression)
    {
        var expr = expression;
        while (true)
            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                break;
            }
            else if (expr.NodeType == ExpressionType.Convert)
            {
                expr = ((UnaryExpression)expr).Operand;
            }
            else if (expr.NodeType == ExpressionType.Call)
            {
                var methodExpr = (MethodCallExpression)expr;
                if (methodExpr.Method.Name == "get_Item") expr = methodExpr.Object;
                break;
            }
            else
            {
                break;
            }

        if (expr.NodeType != ExpressionType.MemberAccess)
            throw new NotSupportedException("请检查表达式,当前表达式Left节点类型不为ExpressionType.MemberAccess");

        var memberExpression = expr as MemberExpression;
        return memberExpression.Type;
    }

    #endregion
}

#endregion