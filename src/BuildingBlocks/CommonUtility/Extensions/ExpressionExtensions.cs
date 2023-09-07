namespace System.Linq.Expressions;

#region 表达式扩展方法

/// <summary>
///     表达式扩展方法
/// </summary>
public static class ExpressionExtensions
{
    #region 表达式And运算

    /// <summary>
    ///     表达式And运算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="leftExpression"></param>
    /// <param name="rightExpression"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExpression,
        Expression<Func<T, bool>> rightExpression)
    {
        var invokedExpr = Expression.Invoke(rightExpression, leftExpression.Parameters);
        return Expression.Lambda<Func<T, bool>>(Expression.And(leftExpression.Body, invokedExpr),
            leftExpression.Parameters);
    }

    #endregion

    #region 表达式OR运算

    /// <summary>
    ///     表达式OR运算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
    }

    #endregion
}

#endregion