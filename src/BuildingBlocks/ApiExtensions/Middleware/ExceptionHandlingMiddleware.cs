using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using Dapr;
using Dapr.Actors;
using DaprTool.BuildingBlocks.Utils;
using DaprTool.BuildingBlocks.Utils.Attributes;
using DaprTool.BuildingBlocks.Utils.Exceptions;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc;


public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment environment)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly IWebHostEnvironment _environment = environment;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, nameof(HandleExceptionAsync));

        var actionDescriptor = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();
        var attribute = actionDescriptor?.ControllerTypeInfo.GetCustomAttribute<PageRequestErrorDefaultResultAttribute>();
        attribute ??= actionDescriptor?.MethodInfo.GetCustomAttribute<PageRequestErrorDefaultResultAttribute>();

        var version = context.GetRequestedApiVersion()?.ToString() ?? "1.0";
        var traceId = context.TraceIdentifier;
        var (errorCode, message) = exception switch
        {
            // TODO: Other Exceptions
            GlobalException ex => (ex.ErrorCode, ex.Message),
            ActorInvokeException ex => (ErrorCode.ActorInvoke, ex.Message),
            DaprApiException ex => (ErrorCode.DaprApi, ex?.InnerException?.Message ?? $"{exception?.Message}"),
            ValidationException ex => (ErrorCode.InvalidArgument, ex.Message),
            _ => (ErrorCode.Unknown, _environment.IsDevelopment() ? $"{exception?.Message} {exception?.StackTrace}" : exception?.Message ?? "")
        };

        ResponseResult<object> response = ResponseResult<object>.Failed(errorCode, message, version, traceId, attribute?.Result);

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    }
}