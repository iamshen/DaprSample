#nullable disable

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using DaprTool.BuildingBlocks.Utils;

namespace System;

#region 字符串相关的扩展方法

/// <summary>
///     字符串 <see cref="string" /> 类型的扩展辅助操作类
/// </summary>
public static class StringExtensions
{
    #region 反序列化

    /// <summary>
    ///     反序列化
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="json">    </param>
    /// <param name="options"> </param>
    /// <returns> </returns>
    public static T Deserialize<T>(this string json, JsonSerializerOptions options = null)
    {
        options ??= new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        return JsonSerializer.Deserialize<T>(json, options);
    }

    #endregion

    #region 是否电子邮件

    /// <summary>
    ///     是否电子邮件
    /// </summary>
    public static bool IsEmail(this string value)
    {
        return RegularHelper.MatchEmail(value);
    }

    #endregion

    #region 指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项

    /// <summary>
    ///     指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项
    /// </summary>
    /// <param name="value">要搜索匹配项的字符串</param>
    /// <param name="pattern">要匹配的正则表达式模式</param>
    /// <param name="isContains">是否包含，否则全匹配</param>
    /// <returns>是完全匹配还是包含,true为包含，fasle为完全匹配</returns>
    public static bool IsMatch(this string value, string pattern, bool isContains = false)
    {
        return RegularHelper.RegularMatch(pattern, value, isContains);
    }

    #endregion

    #region 是否手机号码

    /// <summary>
    ///     是否手机号码
    /// </summary>
    /// <param name="value">      </param>
    /// <param name="isRestrict"> 是否按严格格式验证 </param>
    public static bool IsMobileNumber(this string value, bool isRestrict = false)
    {
        return RegularHelper.MatchMobile(value);
    }

    #endregion

    #region 指示指定的字符串是 null 还是 System.String.Empty 字符串

    /// <summary>
    ///     指示指定的字符串是 null 还是 System.String.Empty 字符串
    /// </summary>
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    #endregion

    #region 指示指定的字符串是 null、空还是仅由空白字符组成。

    /// <summary>
    ///     指示指定的字符串是 null、空还是仅由空白字符组成。
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    #endregion

    #region Base64 编解码

    /// <summary>
    ///     Base64 解码
    /// </summary>
    /// <param name="base64String"> Base64密文 </param>
    /// <returns> </returns>
    public static string FormatBase64String(this string base64String)
    {
        if (!IsBase64String(base64String)) return base64String;

        var base64EncodedBytes = Convert.FromBase64String(base64String);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    /// <summary>
    ///     Base64 编码
    /// </summary>
    /// <param name="plainText"> 需要加密的明文 </param>
    /// <returns> </returns>
    public static string ToBase64String(this string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            plainText = string.Empty;

        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    ///     是否Base64字符串
    /// </summary>
    /// <param name="plainText"> </param>
    /// <returns> </returns>
    public static bool IsBase64String(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return false;

        plainText = plainText.Trim();
        return plainText.Length % 4 == 0 && Regex.IsMatch(plainText, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }

    #endregion
}

#endregion