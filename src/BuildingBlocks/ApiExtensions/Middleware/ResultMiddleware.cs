using System.Reflection;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using DaprTool.BuildingBlocks.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace DaprTool.BuildingBlocks.ApiExtensions.Middleware;


public class ResultMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Call the next middleware in the pipeline
        await next(context);

        // After the response is created, process it
        if (context.Response.HasStarted)
        {
            return;
        }

        var endpoint = context.GetEndpoint();
        var actionDescriptor = endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>();

        if (actionDescriptor != null)
        {
            var controlsProduces = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<ProducesAttribute>();
            var actionProduces = actionDescriptor.MethodInfo.GetCustomAttribute<ProducesAttribute>();
            if (controlsProduces is null && actionProduces is null && !(context.Response.ContentType?.Contains("text/html") ?? false))
            {
                var version = context.GetRequestedApiVersion(1).ToString();
                var traceId = context.TraceIdentifier;
                object returnObject = null;

                if (context.Response.StatusCode == StatusCodes.Status200OK)
                {
                    if (context.Response.Body.Length > 0)
                    {
                        // Assuming the response body is JSON serialized and deserializing it.
                        context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
                        using (var reader = new System.IO.StreamReader(context.Response.Body))
                        {
                            var responseBody = await reader.ReadToEndAsync();
                            returnObject = ResponseResult<object>.Succeed(responseBody, version, traceId);
                        }
                    }
                    else
                    {
                        returnObject = ResponseResult<object>.Succeed(version: version, traceId: traceId);
                    }
                }
                else if (context.Response.StatusCode != StatusCodes.Status204NoContent)
                {
                    returnObject = ResponseResult<object>.Failed(ErrorCode.Unknown, $"Http Code: {context.Response.StatusCode}", version, traceId);
                }

                if (returnObject != null)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(returnObject));
                }
            }
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.

public static class HttpContextExtensions
{
    public static ApiVersion GetRequestedApiVersion(this HttpContext context, int defaultVersion)
    {
        var ver = context.GetRequestedApiVersion();
        return ver ?? new ApiVersion(defaultVersion, 0);
    }
}