using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection;

public static class HealthCheckEndpointRouteBuilderExtensions
{
    public static void MapLivenessHealthChecks(
        this WebApplication app,
        string healthPattern = "/hc",
        string livenessPattern = "/liveness",
        Func<HttpContext, HealthReport, Task>? responseWriter = default)
    {
        var defaultOption = new HealthCheckOptions
        {
            Predicate = _ => true
        };
        if (responseWriter is not null) defaultOption.ResponseWriter = responseWriter;
        app.MapHealthChecks(healthPattern, defaultOption);
        app.MapHealthChecks(livenessPattern, new HealthCheckOptions
        {
            Predicate = r => r.Name.Contains("self")
        });
    }
}