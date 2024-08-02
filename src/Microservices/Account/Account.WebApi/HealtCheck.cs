using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Account.Api;

public class HealtCheck(ILogger<HealtCheck> logger) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Health check passed");
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}
