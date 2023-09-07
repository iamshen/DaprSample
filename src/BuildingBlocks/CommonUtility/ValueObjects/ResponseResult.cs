#nullable disable

namespace DaprTool.BuildingBlocks.CommonUtility.ValueObjects;

#region 请求应答结果

/// <summary>
///     请求应答结果
/// </summary>
public class ResponseResult
{
    #region 方法

    /// <summary>
    ///     创建对象
    /// </summary>
    /// <param name="code"></param>
    /// <param name="version"></param>
    /// <param name="traceId"></param>
    /// <param name="msg"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static ResponseResult Create(int code, string version, string traceId = null, string msg = null,
        object content = null)
    {
        return new ResponseResult
        {
            ErrorCode = code,
            Version = version,
            TraceId = traceId,
            Description = $"{msg}",
            Content = content
        };
    }

    #endregion

    #region 属性

    /// <summary>
    ///     错误码
    /// </summary>

    public int ErrorCode { get; init; }

    /// <summary>
    ///     描述
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    ///     版本号
    /// </summary>
    public string Version { get; init; }

    /// <summary>
    ///     追踪ID
    /// </summary>
    public string TraceId { get; init; }

    /// <summary>
    ///     内容
    /// </summary>
    public object Content { get; init; }

    #endregion
}

#endregion

#region 泛型请求应答结果

/// <summary>
///     泛型请求应答结果
/// </summary>
public class ResponseResult<T> : ResponseResult
{
    #region 属性

    /// <summary>
    ///     内容
    /// </summary>
    public new T Content { get; set; }

    #endregion

    #region 方法

    /// <summary>
    ///     创建对象
    /// </summary>
    /// <param name="code"></param>
    /// <param name="version"></param>
    /// <param name="msg"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static ResponseResult<T> Create(int code, string version, string msg = null, T content = default)
    {
        return new ResponseResult<T>
        {
            ErrorCode = code,
            Version = version,
            Description = $"{msg}",
            Content = content
        };
    }

    #endregion
}

#endregion