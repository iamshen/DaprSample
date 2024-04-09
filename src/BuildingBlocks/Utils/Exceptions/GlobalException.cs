namespace DaprTool.BuildingBlocks.Utils.Exceptions;

#region 统一异常对象

/// <summary>
/// 统一异常对象
/// </summary>
[Serializable]
public class GlobalException : System.Exception
{

    #region 属性

    /// <summary>
    /// 错误编码
    /// </summary>
    public ErrorCode ErrorCode { get; set; }

    #endregion

    #region 初始化

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="message">异常描述</param>
    public GlobalException(string message) : this(ErrorCode.Unknown, message) { }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="errorCode">错误编码</param>
    public GlobalException(ErrorCode errorCode) : this(errorCode, errorCode.GetDisplayName()) { }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="errorCode">错误编码</param>
    /// <param name="message">异常描述</param>
    /// <param name="innerException"></param>
    public GlobalException(ErrorCode errorCode, string message, Exception? innerException = null) : base(message, innerException) => ErrorCode = errorCode;

    #endregion

    #region 异常的字符串形式

    /// <summary>
    /// 异常的字符串形式
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{(int)ErrorCode}|{ErrorCode.GetDisplayName()},{Message}";

    #endregion
}

#endregion
