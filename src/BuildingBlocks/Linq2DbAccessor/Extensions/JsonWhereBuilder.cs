#nullable disable

using System.Linq.Expressions;
using System.Text;
using LinqToDB.Linq;
using LinqToDB.SqlQuery;

namespace LinqToDB.Extensions;

/// <summary>
///     构建JsonWhere表达式
/// </summary>
public static class JsonWhereBuilder
{
    /// <summary>
    ///     左表达式
    /// </summary>
    /// <returns></returns>
    [Sql.Extension("Left", ServerSideOnly = true, BuilderType = typeof(JsonLeftExtensionCallBuilder))]
    public static TSource Left<TSource>(this object source)
    {
        return default;
    }

    /// <summary>
    ///     右表达式
    /// </summary>
    /// <returns></returns>
    [Sql.Extension("Right", ServerSideOnly = true, BuilderType = typeof(JsonRightExtensionCallBuilder))]
    public static TSource Right<TSource>(this object source)
    {
        return default;
    }
}

/// <summary>
/// </summary>
internal class JsonLeftExtensionCallBuilder : Sql.IExtensionCallBuilder
{
    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    public void Build(Sql.ISqExtensionBuilder builder)
    {
        var expression = builder.Arguments[0];
        if (expression.NodeType != ExpressionType.Convert && expression.NodeType != ExpressionType.MemberAccess)
            throw new NotSupportedException($"LambdaExpression Left 节点类型({expression.NodeType})暂不支持");

        var expr = expression;
        while (true)
            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                break;
            }
            else if (expr.NodeType == ExpressionType.Call)
            {
                //支持 IDictionary<>类型
                var methodExpr = (MethodCallExpression)expr;
                if (methodExpr.Method.Name == "get_Item")
                {
                    expr = methodExpr;
                    break;
                }

                throw new NotSupportedException($"LambdaExpression Left 节点方法({methodExpr.Method.Name})暂不支持");
            }
            else
            {
                expr = ((UnaryExpression)expr).Operand;
            }

        if (expr.NodeType == ExpressionType.MemberAccess)
        {
            var memberExpression = expr as MemberExpression;
            var memberPaths = new List<MemberExpression>();
            var current = memberExpression;
            while (true)
                if (current.NodeType == ExpressionType.MemberAccess)
                {
                    memberPaths.Add(current);

                    if (current.Expression.NodeType != ExpressionType.MemberAccess)
                        break;
                    current = current.Expression as MemberExpression;
                }
                else
                {
                    break;
                }

            memberPaths.Reverse();
            memberPaths.RemoveAt(0);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{column}");
            builder.AddParameter("column", builder.ConvertExpressionToSql(memberExpression));

            for (var i = 0; i < memberPaths.Count; i++)
            {
                //todo:复杂JSON对象时,如何处理?
                var isString = memberPaths[i].Type == typeof(string);
                var @operator = isString ? " ->> " : " -> ";
                stringBuilder.Append(@operator);

                var pathName = "path" + i;
                stringBuilder.Append("{" + pathName + "}");
                var keyValue = memberPaths[i].Member.Name;
                builder.AddParameter(pathName, new SqlValue(keyValue));
            }

            builder.Expression = stringBuilder.ToString();
        }
        else if (expr.NodeType == ExpressionType.Call)
        {
            var methodExpr = (MethodCallExpression)expr;
            var column = methodExpr.Object as MemberExpression;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{column}");
            builder.AddParameter("column", builder.ConvertExpressionToSql(column));

            stringBuilder.Append(" ->> ");

            var key = methodExpr.Arguments[0] as ConstantExpression;
            stringBuilder.Append("{key}");
            builder.AddParameter("key", builder.ConvertExpressionToSql(key));

            builder.Expression = stringBuilder.ToString();
        }
        else
        {
            throw new NotSupportedException($"LambdaExpression 节点类型({expression.NodeType})暂不支持");
        }
    }
}

/// <summary>
/// </summary>
internal class JsonRightExtensionCallBuilder : Sql.IExtensionCallBuilder
{
    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    public void Build(Sql.ISqExtensionBuilder builder)
    {
        Query.ClearCaches();

        var expression = builder.Arguments[0];
        var value = Expression.Lambda(expression).Compile().DynamicInvoke().ToString();
        builder.Expression = "{value}";
        builder.AddParameter("value", new SqlValue(value));
    }
}