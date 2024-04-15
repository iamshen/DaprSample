using DaprTool.BuildingBlocks.ApiSwagger;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder;

#region Swagger中间件相关扩展方法

/// <summary>
///     Application Builder Extensions
/// </summary>
public static class WebApplicationExtensions
{
    private const string RoutePrefix = "docs";

    /// <summary>
    ///     UseGoldCloudSwagger
    /// </summary>
    /// <param name="app">           </param>
    /// <param name="configuration"> </param>
    /// <returns> </returns>
    public static void UseAppSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseAppSwagger(configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>()!);
    }


    /// <summary>
    ///     Use GoldCloud Swagger
    /// </summary>
    /// <param name="app">            </param>
    /// <param name="swaggerOptions"> </param>
    private static void UseAppSwagger(this IApplicationBuilder app, SwaggerOptions swaggerOptions)
    {
        if (swaggerOptions?.Enabled != true)
            return;

        var routePrefix = string.IsNullOrWhiteSpace(swaggerOptions.RoutePrefix)
            ? RoutePrefix
            : swaggerOptions.RoutePrefix;

        app.UseSwagger(options => options.RouteTemplate = routePrefix + "/{documentName}/swagger.json")
            .UseSwaggerUI(options =>
            {
                if (swaggerOptions.IsHideSchemas)
                    options.DefaultModelsExpandDepth(-1);

                options.RoutePrefix = routePrefix;

                if (swaggerOptions.Endpoints?.Count > 0)
                    foreach (var endpoint in swaggerOptions.Endpoints)
                        options.SwaggerEndpoint($"/{routePrefix}/{endpoint.Name}/swagger.json", endpoint.Title);
            });
    }
}

#endregion