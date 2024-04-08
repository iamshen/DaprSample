#nullable disable

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.Utils.Converter;

#region TimeSpan? Json序列化转换器

/// <summary>
///     TimeSpan? Json序列化转换器
/// </summary>
public class TimeSpanNullJsonConverter : JsonConverter<TimeSpan?>
{
    #region 字符串转TimeSpan?类型

    /// <summary>
    ///     字符串转TimeSpan?类型
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateStr = reader.GetString();
        if (StringHelper.IsNullOrEmptyOrSpace(dateStr))
            return null;
        return DateTimeOffset.Parse(dateStr).TimeOfDay;
    }

    #endregion

    #region TimeSpan?转Long

    /// <summary>
    ///     TimeSpan?转Long
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNumberValue(0);
        else
            writer.WriteNumberValue(value.Value.Ticks);
    }

    #endregion
}

#endregion