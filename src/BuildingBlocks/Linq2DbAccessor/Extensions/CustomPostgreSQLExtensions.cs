#nullable disable

using System.Linq.Expressions;

namespace LinqToDB;

/// <summary>
///     PostgreSQL 自定义查询函数扩展
/// </summary>
public static class CustomPostgreSQLExtensions
{
    /// <summary>
    ///     查询实体中 jsonb/json 字段中的某个属性(仅限字符串)
    ///     <example>
    ///         <para>例如：</para>
    ///         <code>
    ///             var query = db.XXX.WhereIf(x => x.Authentication.JsonExtractPathText(json => json.RealName) == "张三" );
    ///         </code>
    ///         <para>可以参考 https://github.com/linq2db/linq2db/blob/master/Tests/Linq/Linq/ExpressionsTests.cs </para>
    ///     </example>
    /// </summary>
    /// <typeparam name="TColumn">查询列</typeparam>
    /// <typeparam name="TJsonProp">查询的属性</typeparam>
    /// <param name="field">字段</param>
    /// <param name="path"></param>
    /// <returns></returns>
    [ExpressionMethod(nameof(JsonExtractPathExpression))]
    public static TJsonProp JsonExtractPathText<TColumn, TJsonProp>(this TColumn field,
        Expression<Func<TColumn, TJsonProp>> path) => throw new InvalidOperationException();

    /// <summary>
    ///     表达式转换
    /// </summary>
    /// <typeparam name="TColumn"></typeparam>
    /// <typeparam name="TJsonProp"></typeparam>
    /// <returns></returns>
    private static Expression<Func<TColumn, Expression<Func<TColumn, TJsonProp>>, TJsonProp>>
        JsonExtractPathExpression<TColumn, TJsonProp>()
    {
        return (column, jsonProp)
            => JsonExtractPathText<TColumn, TJsonProp>(column, Sql.Expr<string>(JsonPath(jsonProp)));
    }

    [Sql.Expression("{0}::json #>> {1}", ServerSideOnly = true, IsPredicate = true)]
    private static TJsonProp JsonExtractPathText<TColumn, TJsonProp>(TColumn left, string right)
        => throw new InvalidOperationException();

    private static string JsonPath<TColumn, TJsonProp>(Expression<Func<TColumn, TJsonProp>> extractor)
    {
        var name = string.Empty;
        try
        {
            var expression = GetMemberInfo(extractor);
            name = expression.Member.Name;
        }
        catch
        {
        }

        return "'{" + name + "}'";
    }

    private static MemberExpression GetMemberInfo(Expression method)
    {
        if (method is not LambdaExpression lambda)
            throw new ArgumentNullException(nameof(method));

        MemberExpression memberExpr = null;

        switch (lambda.Body.NodeType)
        {
            case ExpressionType.Convert:
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                break;

            case ExpressionType.MemberAccess:
                memberExpr = lambda.Body as MemberExpression;
                break;
        }

        if (memberExpr is null)
            throw new ArgumentException(nameof(memberExpr));

        return memberExpr;
    }
}