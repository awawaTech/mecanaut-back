using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.EventHandlers;

/// <summary>
/// Siembra métricas globales al iniciar la aplicación.
/// </summary>
public class MetricsSeedHostedService(IServiceProvider services,
                                     ILogger<MetricsSeedHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = services.CreateScope();
        var svc = scope.ServiceProvider.GetRequiredService<IMetricDefinitionCommandService>();
        logger.LogInformation("Seeding default metrics...");
        await svc.Handle(new SeedMetricsCommand());
        logger.LogInformation("Metric seeding finished.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
} 