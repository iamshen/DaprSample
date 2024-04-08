using System.Collections;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.Utils.Converter;

#region Json转换器(动态添加复杂类型的)

/// <summary>
///     Json转换器(动态添加复杂类型的)
/// </summary>
/// <typeparam name="T"></typeparam>
public class DynamicTypeJsonConvert<T> : JsonConverter<T>
{
    #region 是否可以序列化(反序列化)

    /// <summary>
    ///     是否可以序列化(反序列化)
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeof(T) == typeof(object))
        {
            //如果这个转换器的泛型参数为Object，那么当前转换的对象也必须是object才能才能转换
            if (typeToConvert == typeof(object))
                return true;
            return false;
        }

        if (typeof(T).IsAssignableFrom(typeToConvert))
            return true;
        return typeof(T) == typeToConvert;
    }

    #endregion

    #region 从字符串获取类型

    /// <summary>
    ///     从字符串获取类型
    /// </summary>
    /// <param name="moduleName"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Type? GetTypeFromString(string? moduleName)
    {
        if (string.IsNullOrEmpty(moduleName))
            return default;

        if (typeCaches.ContainsKey(moduleName)) return typeCaches[moduleName];

        var newType = Type.GetType(moduleName);
        if (newType is not null)
            typeCaches.AddOrUpdate(moduleName, newType, (k, t) => newType);

        return newType;
    }

    #endregion

    #region 读取数字类型的值

    /// <summary>
    ///     读取数字类型的值
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T? ReadNumber(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (type == typeof(byte) && reader.TryGetByte(out var byteValue) && byteValue is T newByteValue)
            return newByteValue;

        if (type == typeof(sbyte) && reader.TryGetSByte(out var sbyteValue) && sbyteValue is T newSbyteValue)
            return newSbyteValue;

        if (type == typeof(short) && reader.TryGetInt16(out var int16Value) && int16Value is T newInt16Value)
            return newInt16Value;

        if (type == typeof(ushort) && reader.TryGetUInt16(out var uint16Value) && uint16Value is T newUint16Value)
            return newUint16Value;

        if (type == typeof(int) && reader.TryGetInt32(out var int32Value) && int32Value is T newInt32Value)
            return newInt32Value;

        if (type == typeof(uint) && reader.TryGetUInt32(out var uint32Value) && uint32Value is T newUint32Value)
            return newUint32Value;

        if (type == typeof(long) && reader.TryGetInt64(out var int64Value) && int64Value is T newInt64Value)
            return newInt64Value;

        if (type == typeof(ulong) && reader.TryGetUInt64(out var uint64Value) && uint64Value is T newUint64Value)
            return newUint64Value;

        if (type == typeof(decimal) && reader.TryGetDecimal(out var decimalValue) && decimalValue is T newDecimalValue)
            return newDecimalValue;

        if (type == typeof(float) && reader.TryGetSingle(out var floatValue) && floatValue is T newFloatValue)
            return newFloatValue;

        if (type == typeof(double) && reader.TryGetDouble(out var doubleValue) && doubleValue is T newDoubleValue)
            return newDoubleValue;

        //这里type就只能是object类型了，就直接让系统来处理反序列化
        var defaultValue = JsonSerializer.Deserialize(ref reader, type);
        if (defaultValue is not null && defaultValue is T newDefaultValue)
            return newDefaultValue;
        return default;
        ;
    }

    #endregion

    #region 读取字符串类型的值

    /// <summary>
    ///     读取字符串类型的值
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T? ReadString(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (type == typeof(Guid) && reader.TryGetGuid(out var guidValue) && guidValue is T newGuidValue)
            return newGuidValue;

        if (type == typeof(DateTime) && reader.TryGetDateTime(out var dateTimeValue) &&
            dateTimeValue is T newDateTimeValue) return newDateTimeValue;

        if (type == typeof(DateTimeOffset) && reader.TryGetDateTimeOffset(out var dateTimeOffsetValue) &&
            dateTimeOffsetValue is T newDateTimeOffsetValue) return newDateTimeOffsetValue;

        if (type == typeof(string) || type == typeof(char) || type == typeof(BigInteger) || type == typeof(Uri))
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrEmpty(stringValue))
                return default;

            if (type == typeof(string) && stringValue is T newStringValue)
                return newStringValue;

            if (type == typeof(char) && stringValue.FirstOrDefault() is T newCharValue)
                return newCharValue;

            if (type == typeof(BigInteger) && BigInteger.TryParse(stringValue, out var bigIntegerValue) &&
                bigIntegerValue is T newBigIntegerValue)
                return newBigIntegerValue;

            if (type == typeof(Uri) && Uri.TryCreate(stringValue, UriKind.RelativeOrAbsolute, out var uriValue) &&
                uriValue is T newUriValue)
                return newUriValue;

            return default;
        }

        //到这里，要反序列化的类型就只能是byte[]或者Object类型了，直接交给系统反序列化
        var byteObject = JsonSerializer.Deserialize(ref reader, type);
        if (byteObject is T newByteValue)
            return newByteValue;
        return default;
    }

    #endregion

    #region 读取数组

    /// <summary>
    ///     读取数组
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T? ReadArray(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        return ReadObject(ref reader, type, options);
    }

    #endregion

    #region 读取默认值

    /// <summary>
    ///     读取默认值
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T? ReadDefault(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        var defaultResult = JsonSerializer.Deserialize(ref reader, type, options);
        if (defaultResult is not null && defaultResult is T newDefaultResult)
            return newDefaultResult;
        return default;
    }

    #endregion

    #region 写入Object类型

    /// <summary>
    ///     写入Object类型
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteObject(ref Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            var valueType = value.GetType();
            writer.WriteStartObject();
            writer.WritePropertyName(appendTypeName);
            if (valueType.IsAnonymousType())
                writer.WriteStringValue($"{valueType.FullName}");
            else
                writer.WriteStringValue($"{valueType.FullName}, {valueType.Assembly.GetName().Name}");

            var properties = valueType.GetProperties();
            object? propertyValue = null;
            foreach (var item in properties)
            {
                try
                {
                    propertyValue = item.GetValue(value);
                }
                catch
                {
                    propertyValue = null;
                }

                writer.WritePropertyName(item.Name);

                if (propertyValue is not null)
                {
                    var propertyValueType = propertyValue.GetType();

                    if (item.PropertyType == typeof(Type))
                        //如果是Type类型，则就用属性的类型，就不能用属性值的的类型
                    {
                        JsonSerializer.Serialize(writer, propertyValue, item.PropertyType, options);
                    }
                    else if (propertyValueType == valueType)
                        //如果属性值的类型和对象类型一致，则直接序列化，不用递归遍历，防止无限递归
                    {
                        JsonSerializer.Serialize(writer, propertyValue, item.PropertyType, options);
                    }
                    else
                    {
                        /*如果是基元类型(Boolean、Byte、SByte、Int16、UInt16、Int32、UInt32、Int64、UInt64、Char、Double 和 Single)
                         * 或者是string,，或者是枚举类型
                         */
                        if (propertyValueType.IsPrimitive || propertyValueType == typeof(string) ||
                            propertyValueType == typeof(Guid) || propertyValueType == typeof(decimal) ||
                            propertyValueType.IsEnum)
                        {
                            JsonSerializer.Serialize(writer, propertyValue, propertyValueType, options);
                        }

                        ////如果是匿名类型的对象，并且是数组或者集合，测要处理数组或者集合的类型问题
                        else if (typeof(IEnumerable).IsAssignableFrom(propertyValueType))
                        {
                            if (valueType.IsAnonymousType())
                                WriteArray(ref writer, propertyValue, options);
                            else
                                JsonSerializer.Serialize(writer, propertyValue, propertyValueType, options);
                        }

                        //如果是复杂的类型
                        else
                        {
                            WriteObject(ref writer, propertyValue,
                                options); //如果是其他的复杂类型，则递归逐个解析//JsonSerializer.Serialize(writer, propertyValue, propertyValueType, options);
                        }
                    }
                }
                else
                {
                    writer.WriteNullValue();
                }
            }

            writer.WriteEndObject();
        }
    }

    #endregion

    #region 写入数组

    /// <summary>
    ///     写入数组
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteArray(ref Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        var valueType = value?.GetType();
        if (valueType is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteStartObject();
            writer.WritePropertyName(appendTypeName);
            writer.WriteStringValue($"{valueType.FullName}, {valueType.Assembly.GetName().Name}");
            writer.WritePropertyName(appendValueName);
            JsonSerializer.Serialize(writer, value, valueType, options);
            writer.WriteEndObject();
        }
    }

    #endregion

    #region 反序列化

    /// <summary>
    ///     反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override T? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return ReadString(ref reader, type, options);

        if (reader.TokenType == JsonTokenType.Number)
            return ReadNumber(ref reader, type, options);

        if (reader.TokenType == JsonTokenType.StartArray || typeof(IEnumerable).IsAssignableFrom(type))
            return ReadArray(ref reader, type, options);

        if ((reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False) &&
            reader.GetBoolean() is T newBoolValue)
            return newBoolValue;

        if (reader.TokenType == JsonTokenType.Null || reader.TokenType == JsonTokenType.None)
            return default;

        if (reader.TokenType == JsonTokenType.StartObject)
            return ReadObject(ref reader, type, options);

        return ReadDefault(ref reader, type, options);
    }

    #endregion

    #region 序列化

    /// <summary>
    ///     序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            var valueType = value.GetType();
            if (valueType == typeof(bool) && value is bool newBoolValue)
                writer.WriteBooleanValue(newBoolValue);

            else if (valueType == typeof(char) && value is char newCharValue)
                writer.WriteStringValue(newCharValue.ToString());

            else if (valueType == typeof(byte) && value is byte newByteValue)
                writer.WriteNumberValue(newByteValue);

            else if (valueType == typeof(sbyte) && value is sbyte newSbyteValue)
                writer.WriteNumberValue(newSbyteValue);

            else if (valueType == typeof(short) && value is short newShortValue)
                writer.WriteNumberValue(newShortValue);

            else if (valueType == typeof(ushort) && value is ushort newUshortValue)
                writer.WriteNumberValue(newUshortValue);

            else if (valueType == typeof(int) && value is int newIntValue)
                writer.WriteNumberValue(newIntValue);

            else if (valueType == typeof(uint) && value is uint newUintValue)
                writer.WriteNumberValue(newUintValue);

            else if (valueType == typeof(long) && value is long newLongValue)
                writer.WriteNumberValue(newLongValue);

            else if (valueType == typeof(ulong) && value is ulong newUlongValue)
                writer.WriteNumberValue(newUlongValue);

            else if (valueType == typeof(BigInteger) && value is BigInteger newBigIntegerValue)
                writer.WriteStringValue(newBigIntegerValue.ToString());

            else if (valueType == typeof(decimal) && value is decimal newDecimalValue)
                writer.WriteNumberValue(newDecimalValue);

            else if (valueType == typeof(float) && value is float newFloatValue)
                writer.WriteNumberValue(newFloatValue);

            else if (valueType == typeof(double) && value is double newDoubleValue)
                writer.WriteNumberValue(newDoubleValue);

            else if (valueType.IsEnum)
                JsonSerializer.Serialize(writer, value);

            else if (valueType == typeof(string) && value is string newStringValue)
                writer.WriteStringValue(newStringValue);

            else if (valueType == typeof(Uri))
                JsonSerializer.Serialize(writer, value, options);

            else if (valueType == typeof(Guid))
                JsonSerializer.Serialize(writer, value, options);

            else if (valueType == typeof(TimeSpan))
                JsonSerializer.Serialize(writer, value, options);

            else if (valueType == typeof(DateTime) || valueType == typeof(DateTimeOffset))
                JsonSerializer.Serialize(writer, value, options);

            else if (valueType == typeof(JsonElement) && value is JsonElement jsonElement)
                jsonElement.WriteTo(writer); //还可以通过遍历jsonElement.EnumerateObject()来一次处理每一个值

            //如果是集合或者数组,string也是继承了集合IEnumerable,因此需要排除掉
            else if (typeof(IEnumerable).IsAssignableFrom(valueType) && valueType != typeof(string))
                WriteArray(ref writer, value, options);

            else
                WriteObject(ref writer, value, options);
        }
    }

    #endregion

    #region 私有变量

    /// <summary>
    ///     附加到序列化JSON中的类型属性名
    /// </summary>
    private const string appendTypeName = "$type";

    /// <summary>
    ///     附加到序列化JSON中的值属性名
    /// </summary>
    private const string appendValueName = "$value";

    /// <summary>
    ///     类型缓存
    /// </summary>
    private readonly ConcurrentDictionary<string, Type> typeCaches = new();

    #endregion

    #region 读取Object类型的值

    /// <summary>
    ///     读取Object类型的值
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T? ReadObject(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var jsonDocument) || jsonDocument is null)
            return default;

        var jsonElement = jsonDocument.RootElement.Clone();
        var result = ReadObject(jsonElement, type, options);
        jsonDocument.Dispose();

        if (result is null)
            return default;
        return (T)result;
    }

    /// <summary>
    ///     从JsonElement中解析出属性名称和值
    /// </summary>
    /// <param name="jsonElement"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private object? ReadObject(JsonElement jsonElement, Type? type, JsonSerializerOptions options)
    {
        var moduleName = string.Empty;
        if (jsonElement.TryGetProperty(appendTypeName, out var property))
            moduleName = property.GetString();

        object? instance;
        var moduleType = GetTypeFromString(moduleName);
        moduleType ??= type ?? typeof(object);

        if (moduleType == typeof(object))
        {
            var dynamicObject = new ExpandoObject();
            var jsonProperties = jsonElement.EnumerateObject();
            while (jsonProperties.MoveNext())
                if (jsonProperties.Current.Name != appendTypeName)
                {
                    dynamicObject.TryAdd(jsonProperties.Current.Name,
                        ReadValue(jsonProperties.Current.Value, null, options));
                }
                else
                {
                    var typeString = jsonProperties.Current.Value.GetString();
                    var valueType = GetTypeFromString(typeString);
                    if (jsonProperties.MoveNext())
                        dynamicObject.TryAdd(jsonProperties.Current.Name,
                            ReadValue(jsonProperties.Current.Value, valueType, options));
                }

            instance = dynamicObject;
        }
        else
        {
            //尝试获取$value字段，如果获取到了，说明是数组或者集合类型，则要获取真正的集合数据然后在反序列化
            if (jsonElement.TryGetProperty(appendValueName, out var valueProperty))
                instance = valueProperty.Deserialize(moduleType, options);
            else
                instance = jsonElement.Deserialize(moduleType, options);
        }

        return instance;
    }

    /// <summary>
    ///     从JsonElement中解析数属性值
    /// </summary>
    /// <param name="jsonElement"></param>
    /// <param name="options"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private object? ReadValue(JsonElement jsonElement, Type? type, JsonSerializerOptions options)
    {
        if (jsonElement.ValueKind == JsonValueKind.Null) return null;

        if (jsonElement.ValueKind == JsonValueKind.Object) return ReadObject(jsonElement, null, options);

        if (jsonElement.ValueKind == JsonValueKind.String) return jsonElement.GetString();

        if (jsonElement.ValueKind == JsonValueKind.False || jsonElement.ValueKind == JsonValueKind.True)
            return jsonElement.GetBoolean();

        if (jsonElement.ValueKind == JsonValueKind.Array)
        {
            IList<object?> list = new List<object?>();
            foreach (var item in jsonElement.EnumerateArray())
                list.Add(ReadValue(item, type, options));

            return list;
        }

        if (jsonElement.ValueKind == JsonValueKind.Number)
        {
            if (jsonElement.TryGetByte(out var byteValue))
                return byteValue;

            if (jsonElement.TryGetSByte(out var sbyteValue))
                return sbyteValue;

            if (jsonElement.TryGetInt16(out var int16Value))
                return int16Value;

            if (jsonElement.TryGetUInt16(out var uint16Value))
                return uint16Value;

            if (jsonElement.TryGetInt32(out var int32Value))
                return int32Value;

            if (jsonElement.TryGetUInt32(out var uint32Value))
                return uint32Value;

            if (jsonElement.TryGetInt64(out var int64Value))
                return int64Value;

            if (jsonElement.TryGetUInt64(out var uint64Value))
                return uint64Value;

            if (jsonElement.TryGetDecimal(out var decimalValue))
                return decimalValue;

            if (jsonElement.TryGetSingle(out var floatValue))
                return floatValue;

            if (jsonElement.TryGetDouble(out var doubleValue))
                return doubleValue;
            return 0;
        }

        return null;
    }

    #endregion
}

#endregion