using DaprTool.BuildingBlocks.Utils;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc.Filters;

public class ModelStateFilter : IActionFilter
{
    #region Action执行之后的事件

    /// <summary>
    ///     Action执行之后的事件
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    #endregion

    #region Action执行前检查数据模型是否通过验证

    /// <summary>
    ///     Action执行前检查数据模型是否通过验证
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var version = context.HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
        var traceId = context.HttpContext.TraceIdentifier;
        context.Result = new OkObjectResult(ResponseResult<object>.Failed(ErrorCode.ArgumentException, version, traceId,
            context.ModelState.GetErrorMessage()));
    }

    #endregion
}

public static class ModelStateExtensions
{
    #region 获取验证模型的错误消息

    /// <summary>
    ///     获取验证模型的错误消息
    /// </summary>
    /// <param name="state"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    public static string GetErrorMessage(this ModelStateDictionary state, string split = ";")
    {
        var errors = state
            .Where(e => e.Value != null && e.Value.Errors.Any())
            .Select(m
                => $"{string.Join(",", m.Value?.Errors?.Select(error => error.ErrorMessage) ?? new[] { "" })} [{m.Key}]");

        return string.Join(split, errors);
    }

    #endregion
}