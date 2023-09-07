#nullable disable

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.CommonUtility.Converter;

#region 时间Json序列化转换器

/// <summary>
///     时间Json序列化转换器
/// </summary>
public class DateTimeNullJsonConverter : JsonConverter<DateTimeOffset?>
{
    #region 属性

    /// <summary>
    ///     时间转字符串的格式
    /// </summary>
    public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

    #endregion

    #region 字符串转时间类型

    /// <summary>
    ///     字符串转时间类型
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateStr = reader.GetString();
        if (StringHelper.IsNullOrEmptyOrSpace(dateStr))
            return null;
        return DateTimeOffset.Parse(dateStr);
    }

    #endregion

    #region 时间转字符串

    /// <summary>
    ///     时间转字符串
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteStringValue("");
        else
            writer.WriteStringValue(value.Value.ToString(DateTimeFormat));
    }

    #endregion

    #region 初始化

    /// <summary>
    ///     时间Json序列化转换器
    /// </summary>
    public DateTimeNullJsonConverter()
    {
    }

    /// <summary>
    ///     时间Json序列化转换器
    /// </summary>
    /// <param name="dateTimeFormat"></param>
    public DateTimeNullJsonConverter(string dateTimeFormat)
    {
        if (!StringHelper.IsNullOrEmptyOrSpace(dateTimeFormat))
            DateTimeFormat = dateTimeFormat;
    }

    #endregion
}

#endregion