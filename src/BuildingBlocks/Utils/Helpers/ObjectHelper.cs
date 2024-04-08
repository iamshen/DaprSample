namespace DaprTool.BuildingBlocks.Utils;

#region Object的助手类

/// <summary>
///     Object的助手类
/// </summary>
public class ObjectHelper
{
    #region 将对象的属性转为Url的Key=Value的字符串

    /// <summary>
    ///     将对象的属性转为Url的Key=Value的字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ObjectToUrlString(object value)
    {
        if (value is null)
            return "";

        var properties = value.GetType().GetProperties();
        if (properties != null && properties.Length > 0)
        {
            var argKeyValue = properties.Select(m => $"{m.Name}={m.GetValue(value)}");
            return string.Join("&", argKeyValue);
        }

        return "";
    }

    #endregion
}

#endregion