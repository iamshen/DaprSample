using System.Linq.Expressions;
using System.Text.Json;
using LinqToDB.Common;

namespace DaprTool.BuildingBlocks.Linq2DbAccessor.Converter;

/// <summary>JSON和字符串的转换器</summary>
/// <typeparam name="T"></typeparam>
public class JsonValueConverter<T> : ValueConverter<T?, string?>
{
    /// <summary>JSON和字符串的转换器</summary>
    public JsonValueConverter()
        : this(null)
    {
    }

    /// <summary>JSON和字符串的转换器</summary>
    public JsonValueConverter(JsonSerializerOptions? serializeOptions = null,
        JsonSerializerOptions? deserializeOptions = null)
        : base(v => v == null ? default : JsonSerializer.Serialize<T>(v, serializeOptions),
            (Expression<Func<string?, T?>>)(v =>
                v == null ? default : JsonSerializer.Deserialize<T>(v, deserializeOptions)), true)
    {
    }
}