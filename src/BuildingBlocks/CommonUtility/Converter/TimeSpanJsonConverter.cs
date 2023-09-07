#nullable disable

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.CommonUtility.Converter;

#region TimeSpan Json序列化转换器

/// <summary>
///     TimeSpan Json序列化转换器
/// </summary>
public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    #region 字符串转时间类型

    /// <summary>
    ///     字符串转TimeSpan类型
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTimeOffset.Parse(reader.GetString()).TimeOfDay;
    }

    #endregion

    #region TimeSpan?转Long

    /// <summary>
    ///     TimeSpan?转Long
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Ticks);
    }

    #endregion
}

#endregion