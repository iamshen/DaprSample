namespace DaprTool.BuildingBlocks.CommonUtility;

#region 时间相关助手类

/// <summary>
///     时间相关助手类
/// </summary>
public class DateTimeHelper
{
    #region 时间戳转本地时间的字符串

    /// <summary>
    ///     时间戳(毫秒)转本地时间的字符串
    /// </summary>
    /// <param name="timestamp">时间戳(毫秒)</param>
    /// <param name="format">字符串格式</param>
    /// <returns></returns>
    public static string FromMilliSecondsToLocalTime(long timestamp, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).ToLocalTime().ToString(format);
    }

    /// <summary>
    ///     时间戳(秒)转本地时间的字符串
    /// </summary>
    /// <param name="timestamp">时间戳(秒)</param>
    /// <param name="format">字符串格式</param>
    /// <returns></returns>
    public static string FromSecondsToLocalTime(long timestamp, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return DateTimeOffset.FromUnixTimeSeconds(timestamp).ToLocalTime().ToString(format);
    }

    #endregion
}

#endregion