#nullable disable

using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace System;

/// <summary>
///     Object 扩展
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    ///     将对象类型转换为指定类型
    /// </summary>
    /// <param name="value">当前Object对象</param>
    /// <param name="conversionType">需要转换的类型</param>
    /// <returns>返回转换后的Object（目标类型）</returns>
    public static object CastTo(this object value, Type conversionType)
    {
        if (value == null)
            return null;
        if (conversionType.IsNullableType())
            conversionType = conversionType.GetUnNullableType();
        if (conversionType.IsEnum)
            return Enum.Parse(conversionType, value.ToString());
        if (conversionType == typeof(Guid))
            return Guid.Parse(value.ToString());
        return Convert.ChangeType(value, conversionType);
    }

    /// <summary>
    ///     将对象转换为指定类型
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="value">要转换的对象</param>
    /// <returns>返回转换后的目标类型</returns>
    public static T CastTo<T>(this object value)
    {
        if (value == null || default(T) == null) return default;
        if (value.GetType() == typeof(T)) return (T)value;
        var result = CastTo(value, typeof(T));
        return (T)result;
    }

    /// <summary>
    ///     将对象类型转换为指定类型，如转换失败返回指定的默认值
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="value">要转换的对象</param>
    /// <param name="defaultValue">转换失败返回指定的默认值</param>
    /// <returns></returns>
    public static T CastTo<T>(this object value, T defaultValue)
    {
        try
        {
            return CastTo<T>(value);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     序列化
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string Serialize(this object obj, JsonSerializerOptions options = null)
    {
        options ??= new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        return JsonSerializer.Serialize(obj, options);
    }
}