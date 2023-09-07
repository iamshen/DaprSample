global using System;
global using Dapr.Client;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using System.Threading;
global using System.Threading.Tasks;

namespace DaprTool.HealthChecks;

public class DaprHealthCheck : IHealthCheck
{
    private readonly DaprClient _daprClient;

    public DaprHealthCheck(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var healthy = await _daprClient.CheckHealthAsync(cancellationToken);
            
        if (healthy)
        {
            return HealthCheckResult.Healthy("Dapr sidecar is healthy.");
        }

        return new HealthCheckResult(context.Registration.FailureStatus, "Dapr sidecar is unhealthy.");
    }
}
