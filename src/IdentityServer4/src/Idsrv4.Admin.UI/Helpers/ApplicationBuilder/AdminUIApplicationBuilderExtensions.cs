using System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Idsrv4.Admin.UI.Configuration;
using Idsrv4.Admin.UI.Configuration.Constants;
using Idsrv4.Admin.UI.Helpers;

namespace Microsoft.AspNetCore.Builder;

public static class AdminUiApplicationBuilderExtensions
{
    /// <summary>
    ///     Adds the IdentityServer4 Admin UI to the pipeline of this application. This method must be called
    ///     between UseRouting() and UseEndpoints().
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseIdentityServer4AdminUi(this IApplicationBuilder app)
    {
        app.UseRoutingDependentMiddleware(app.ApplicationServices.GetRequiredService<TestingConfiguration>());

        return app;
    }

    /// <summary>
    ///     Maps the IdentityServer4 Admin UI to the routes of this application.
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="patternPrefix"></param>
    public static IEndpointConventionBuilder
        MapIdentityServer4AdminUi(this IEndpointRouteBuilder endpoint, string patternPrefix = "/")
        => endpoint.MapAreaControllerRoute(CommonConsts.AdminUIArea, CommonConsts.AdminUIArea,
            patternPrefix + "{controller=Home}/{action=Index}/{id?}");

}