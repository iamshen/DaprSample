using DaprTool.BuildingBlocks.ApiExtensions.Middleware;

namespace Microsoft.AspNetCore.Builder;

public static class ResultMiddlewareExtensions
{
    public static IApplicationBuilder UseResultMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ResultMiddleware>();
    }
}