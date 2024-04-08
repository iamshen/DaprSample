namespace System
{
    #region TimeSpan相关的扩展类

    /// <summary>
    /// TimeSpan相关的扩展类
    /// </summary>
    public static class TimeSpanExtensions
    {
        #region 转字符串

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static string ToFriendlyString(this TimeSpan timeSpan)
            => timeSpan.ToString(@"hh\:mm\:ss");

        #endregion
    }

    #endregion
}
