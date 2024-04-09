
namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

/// <summary>
///     统一返回结果
/// </summary>
[Serializable]
public record ResponseResult<T>
{
    /// <summary>
    ///     错误码
    /// </summary>

    public ErrorCode ErrorCode { get; init; }

    /// <summary>
    ///     错误码字符串
    /// </summary> 
    public string ErrorCodeStraight => ErrorCode.ToString();

    /// <summary>
    ///     描述
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    ///     版本号
    /// </summary>
    public string Version { get; init; } = "1.0";

    /// <summary>
    ///     追踪ID
    /// </summary>
    public string TraceId { get; init; } = string.Empty;

    /// <summary>
    ///     内容
    /// </summary>
    public T? Content { get; init; } = default;


    /// <summary>
    ///     返回失败。
    /// </summary>
    /// <param name="message"></param>
    /// <param name="version"></param>
    /// <param name="traceId"></param>
    /// <param name="data"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static ResponseResult<T> Failed(ErrorCode code = ErrorCode.Unknown, string message = "", string version = "1.0", string traceId = "", T? data = default)
    {
        return new ResponseResult<T>
        {
            ErrorCode = code,
            Description = message,
            Content = data,
            TraceId = traceId,
            Version = version
        };
    }

    /// <summary>
    ///     返回成功。
    /// </summary>
    /// <returns></returns>
    public static ResponseResult<T> Succeed(T? data = default, string message = "成功", string version = "1.0", string traceId = "")
    {
        return new ResponseResult<T>
        {
            ErrorCode = ErrorCode.Success,
            Description = message,
            Content = data,
            TraceId = traceId,
            Version = version
        };
    }

}

