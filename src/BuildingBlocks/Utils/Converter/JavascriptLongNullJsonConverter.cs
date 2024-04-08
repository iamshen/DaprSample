using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.Utils.Converter;

#region JavaScript长整型和字符串的转换器

/// <summary>
///     JavaScript长整型和字符串的转换器
/// </summary>
public class JavascriptLongNullJsonConverter : JsonConverter<long?>
{
    #region string转Long

    /// <summary>
    ///     string转Long
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.String:
            {
                var value = reader.GetString()?.Trim();
                return string.IsNullOrEmpty(value)
                    ? throw new InvalidOperationException("Cannot get the value of a token type 'Number' as a string")
                    : long.Parse(value ?? string.Empty);
            }
            case JsonTokenType.Number:
                return reader.GetInt64();
            default:
                throw new InvalidOperationException("Cannot get the value of a token type 'Number' as a string");
        }
    }

    #endregion

    #region long转为string

    /// <summary>
    ///     long转为string
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"{value}");
    }

    #endregion
}

#endregion