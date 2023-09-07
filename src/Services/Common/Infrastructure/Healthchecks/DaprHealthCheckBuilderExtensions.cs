using DaprTool.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection;

public static class DaprHealthCheckBuilderExtensions
{
    public static IHealthChecksBuilder AddDapr(this IHealthChecksBuilder builder)
    {
        return builder.AddCheck<DaprHealthCheck>("dapr");
    }
}