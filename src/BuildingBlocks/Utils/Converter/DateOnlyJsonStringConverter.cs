using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaprTool.BuildingBlocks.Utils.Converter
{
    #region DateOnly JSON转换器(字符串)

    /// <summary>
    /// DateOnly JSON转换器(字符串)
    /// </summary>
    public class DateOnlyJsonStringConverter : JsonConverter<DateOnly>
    {
        #region 私有变量

        /// <summary>
        /// 转换的字符串格式
        /// </summary>
        private readonly string? format = "yyyy-MM-dd";

        #endregion

        #region 初始化

        /// <summary>
        /// DateOnly JSON转换器(字符串)
        /// </summary>
        public DateOnlyJsonStringConverter() { }

        /// <summary>
        /// DateOnly JSON转换器(字符串)
        /// </summary>
        /// <param name="format"></param>
        public DateOnlyJsonStringConverter(string? format)
            => this.format = format;

        #endregion

        #region 反序列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return DateOnly.FromDayNumber(0);
                case JsonTokenType.String when DateOnly.TryParse(reader.GetString(), out var resultDate):
                    return resultDate;
                default:
                    throw new NotSupportedException($"{reader.TokenType}反序列化不被支持");
            }
        }

        #endregion

        #region 序列化

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(this.format));

        #endregion
    }

    #endregion
}
