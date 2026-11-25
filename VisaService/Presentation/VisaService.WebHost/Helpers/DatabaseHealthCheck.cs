using Microsoft.Extensions.Diagnostics.HealthChecks;
using VisaService.Infrastructure.EntityFramework;

namespace VisaService.WebHost.Helpers;

/// <summary>Проверка доступности PostgreSQL для эндпоинта /health.</summary>
public class DatabaseHealthCheck(ApplicationDbContext context) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext healthCheckContext, CancellationToken cancellationToken = default)
        => await context.Database.CanConnectAsync(cancellationToken)
            ? HealthCheckResult.Healthy("PostgreSQL доступен")
            : HealthCheckResult.Unhealthy("Нет подключения к PostgreSQL");
}
