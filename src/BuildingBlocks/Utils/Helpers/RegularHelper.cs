using System.Text.RegularExpressions;
using DaprTool.BuildingBlocks.Utils.Constant;

namespace DaprTool.BuildingBlocks.Utils;

#region 正则表达式助手类

/// <summary>
///     正则表达式助手类
/// </summary>
public class RegularHelper
{
    #region 检查是否是指定的类型的字符串

    /// <summary>
    ///     检查是否是合法的Http或者https的url
    /// </summary>
    /// <param name="text">要检查的字符串</param>
    /// <returns></returns>
    public static bool MatchHttpUrl(string text)
    {
        return RegularMatch(RegularExpressions.HttpUrl, text);
    }

    /// <summary>
    ///     检查是否是合法的Http或者https的url
    /// </summary>
    /// <param name="text">要检查的字符串</param>
    /// <param name="options">匹配选项</param>
    /// <returns></returns>
    public static bool MatchHttpUrl(string text, RegexOptions options)
    {
        return RegularMatch(RegularExpressions.HttpUrl, text, options);
    }

    /// <summary>
    ///     是否是合法的手机号码
    /// </summary>
    /// <param name="mobile">手机号码</param>
    /// <returns></returns>
    public static bool MatchMobile(string mobile)
    {
        return RegularMatch(RegularExpressions.Mobile, mobile);
    }

    /// <summary>
    ///     是否是合法的手机号码
    /// </summary>
    /// <param name="mobile">手机号码</param>
    /// <param name="options">匹配选项</param>
    /// <returns></returns>
    public static bool MatchMobile(string mobile, RegexOptions options)
    {
        return RegularMatch(RegularExpressions.Mobile, mobile, options);
    }

    /// <summary>
    ///     是否合法邮件地址
    /// </summary>
    /// <param name="email">邮件地址</param>
    /// <returns></returns>
    public static bool MatchEmail(string email)
    {
        return RegularMatch(RegularExpressions.Email, email);
    }

    /// <summary>
    ///     是否合法邮件地址
    /// </summary>
    /// <param name="email">邮件地址</param>
    /// <param name="options">匹配选项</param>
    /// <returns></returns>
    public static bool MatchEmail(string email, RegexOptions options)
    {
        return RegularMatch(RegularExpressions.Email, email, options);
    }

    /// <summary>
    ///     是否合法IP
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool MatchIp(string ip)
    {
        return RegularMatch(RegularExpressions.Ip, ip);
    }

    /// <summary>
    ///     是否合法IP
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static bool MatchIp(string ip, RegexOptions options)
    {
        return RegularMatch(RegularExpressions.Ip, ip, options);
    }

    #endregion

    #region 检查字符串是否和正则表达式匹配

    /// <summary>
    ///     检查字符串是否和正则表达式匹配
    /// </summary>
    /// <param name="regex">正则表达式</param>
    /// <param name="text">要验证的字符串</param>
    /// <param name="isContains">是完全匹配还是包含,true为包含，fasle为完全匹配</param>
    /// <returns></returns>
    public static bool RegularMatch(string regex, string text, bool isContains = false)
    {
        return isContains ? Regex.Match(text, regex).Success : Regex.IsMatch(text, regex);
    }

    /// <summary>
    ///     检查字符串是否和正则表达式匹配
    /// </summary>
    /// <param name="regex">正则表达式</param>
    /// <param name="text">要验证的字符串</param>
    /// <param name="options">匹配模式选项</param>
    /// <param name="isContains">是完全匹配还是包含,true为包含，fasle为完全匹配</param>
    /// <returns></returns>
    public static bool RegularMatch(string regex, string text, RegexOptions options, bool isContains = false)
    {
        return isContains ? Regex.Match(text, regex).Success : Regex.IsMatch(text, regex, options);
    }

    #endregion
}

#endregion