// -----------------------------------------------------------------------
//  <last-editor>黄深</last-editor>
//  <last-date>2022-10-24 9:51</last-date>
// -----------------------------------------------------------------------

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
///     应用常量
/// </summary>
public static class AppConstants
{
    #region Api GroupNames

    /// <summary>mobile </summary>
    public const string MobileGroup = "mobile";

    /// <summary>open </summary>
    public const string OpenGroup = "open";

    /// <summary> default </summary>
    public const string DefaultGroup = "default";

    #endregion




    /// <summary>
    /// 输出类型
    /// </summary>
    public const string ResponseJsonContentType = "application/json";

    /// <summary>
    /// 路由中版本号的变量key
    /// </summary>
    public const string WebApiVersionKey = "version";

    /// <summary>
    /// 请求头中放的追踪ID的Key
    /// </summary>
    public const string RequestTraceIdKey = "Request-Trace-Id";
}