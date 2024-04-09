using System.Reflection;
using DaprTool.BuildingBlocks.Utils;
using DaprTool.BuildingBlocks.Utils.Exceptions;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.AspNetCore.Mvc.Filters;

#region 统一返回格式的内容的Fileter

/// <summary>
/// 统一返回格式的内容的Fileter
/// </summary>
public class ResultFilter : ProducesAttribute
{
    #region 初始化

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="contentType"></param>
    /// <param name="additionalContentTypes"></param>
    public ResultFilter(string contentType, params string[] additionalContentTypes) : base(contentType, additionalContentTypes)
    {
    }

    #endregion

    #region Action执行后的事件

    /// <summary>
    /// Action执行后的事件
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuted(ResultExecutedContext context)
        => base.OnResultExecuted(context);

    #endregion

    #region Action执行前的事件

    /// <summary>
    /// Action执行前的事件
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var controllProduces = controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttribute<ProducesAttribute>();
        var actionProduces = controllerActionDescriptor?.MethodInfo.GetCustomAttribute<ProducesAttribute>();
        if (controllProduces is null && actionProduces is null && context.Result is not ViewResult && context.Result is not PageResult)
        {
            var version = context.HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
            var traceId = context.HttpContext.TraceIdentifier;
            object? returnObject;
            if (context.Result is ObjectResult objectResult)
            {
                returnObject = objectResult.Value switch
                {
                    GlobalException goldCloudException => ResponseResult<object>.Failed(goldCloudException.ErrorCode, goldCloudException.Message, version, traceId),
                    Exception exception => ResponseResult<object>.Failed(ErrorCode.Unknown, exception.Message, version, traceId),
                    ProblemDetails problemDetails => ResponseResult<object>.Failed(ErrorCode.Unknown, $"{problemDetails.Title} StatusCode {problemDetails.Status}", version, traceId),
                    _ => ResponseResult<object>.Succeed(data: objectResult.Value, version: version, traceId: traceId),
                };
            }

            else if (context.Result is OkResult)
                returnObject = ResponseResult<object>.Succeed(version: version, traceId: traceId);

            else if (context.Result is OkObjectResult okObjectResult)
                returnObject = ResponseResult<object>.Succeed(okObjectResult.Value, version: version, traceId: traceId);

            else if (context.Result is EmptyResult)
                returnObject = ResponseResult<object>.Succeed(version: version, traceId: traceId);

            else if (context.Result is ContentResult contentResult)
                returnObject = ResponseResult<object>.Succeed(contentResult.Content, version: version, traceId: traceId);


            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                returnObject = statusCodeResult.StatusCode == 200
                    ? ResponseResult<object>.Succeed(version: version, traceId: traceId)
                    : ResponseResult<object>.Failed(ErrorCode.Unknown, $"{ErrorCode.Unknown.GetDisplayName()} Http Code:{statusCodeResult.StatusCode}", version, traceId);
            }

            else
                returnObject = ResponseResult<object>.Failed(ErrorCode.Unknown, version, traceId, ErrorCode.Unknown.GetDisplayName());

            context.Result = new ObjectResult(returnObject);
        }
    }

    #endregion
}

#endregion