namespace DaprTool.BuildingBlocks.Utils.Constant;

#region 正则表达式定义

/// <summary>
///     正则表达式定义
/// </summary>
public class RegularExpressions
{
    /// <summary>
    ///     匹配URL(只匹配http,https)
    /// </summary>
    public const string HttpUrl = @"^((https|http)?:\/\/)[^\s]+$";

    /// <summary>
    ///     手机号码(国内)
    /// </summary>
    public const string Mobile = @"(13[0-9]|14[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9]|19[0-9])\d{8}$";

    /// <summary>
    ///     身份证号码(国内)
    /// </summary>
    public const string IdCardNo =
        @"^[1-9]\d{5}(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$";

    /// <summary>
    ///     只包含字符或数字
    /// </summary>
    public const string CharacterOrNum = @"^[a-zA-Z0-9]+$";

    /// <summary>
    ///     只匹配数字组成的字符串
    /// </summary>
    public const string Number = @"^[0-9]+$";

    /// <summary>
    ///     匹配正整数
    /// </summary>
    public const string PositiveNum = @"^[1-9]\d*$";

    /// <summary>
    ///     匹配正整数和零
    /// </summary>
    public const string PositiveNumAndZero = @"^[0-9]\d*$";

    /// <summary>
    ///     匹配正浮点数和零
    /// </summary>
    public const string MatchPositiveFloat = @"^\d+(\.\d+)?$";

    /// <summary>
    ///     匹配电子邮件
    /// </summary>
    public const string Email = @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}$";

    /// <summary>
    ///     IP地址
    /// </summary>
    public const string Ip =
        @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
}

#endregion