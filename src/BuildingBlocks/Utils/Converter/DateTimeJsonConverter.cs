#nullable disable

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.Utils.Converter;

#region 时间Json序列化转换器

/// <summary>
///     时间Json序列化转换器
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTimeOffset>
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
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTimeOffset.Parse(reader.GetString());
    }

    #endregion

    #region 时间转字符串

    /// <summary>
    ///     时间转字符串
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateTimeFormat));
    }

    #endregion

    #region 初始化

    /// <summary>
    ///     时间Json序列化转换器
    /// </summary>
    public DateTimeJsonConverter()
    {
    }

    /// <summary>
    ///     时间Json序列化转换器
    /// </summary>
    /// <param name="dateTimeFormat"></param>
    public DateTimeJsonConverter(string dateTimeFormat)
    {
        if (!StringHelper.IsNullOrEmptyOrSpace(dateTimeFormat))
            DateTimeFormat = dateTimeFormat;
    }

    #endregion
}

#endregion