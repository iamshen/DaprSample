using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Mvc;

public static class ExceptionHandlingMiddlewareExtensions
{
    /// <summary>
    ///    Global error handler
    /// </summary>
    /// <param name="app"></param>
    /// <param name="enable"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app, bool enable = true)
    {
        if (app == null)
            throw new ArgumentNullException(nameof(app));

        if (!enable) return app;

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}
