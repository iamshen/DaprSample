using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.Utils.Converter;

#region TimeOnly JSON转换器(字符串)

/// <summary>
///     TimeOnly JSON转换器(字符串)
/// </summary>
public class TimeOnlyJsonStringConverter : JsonConverter<TimeOnly>
{
    #region 私有变量

    /// <summary>
    ///     转换的字符串格式
    /// </summary>
    private readonly string? format = "HH:mm:ss";

    #endregion

    #region 发序列化

    /// <summary>
    ///     发序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return TimeOnly.FromTimeSpan(TimeSpan.Zero);
            case JsonTokenType.String when TimeOnly.TryParse(reader.GetString(), out var resultTime):
                return resultTime;
            default:
                throw new NotSupportedException($"{reader.TokenType}反序列化不被支持");
        }
    }

    #endregion

    #region 序列化

    /// <summary>
    ///     序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }

    #endregion

    #region 初始化

    /// <summary>
    ///     TimeOnly JSON转换器(字符串)
    /// </summary>
    public TimeOnlyJsonStringConverter()
    {
    }

    /// <summary>
    ///     TimeOnly JSON转换器(字符串)
    /// </summary>
    /// <param name="format"></param>
    public TimeOnlyJsonStringConverter(string? format)
    {
        this.format = format;
    }

    #endregion
}

#endregion