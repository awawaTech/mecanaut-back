using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.CommandServices;

public class MetricDefinitionCommandService : IMetricDefinitionCommandService
{
    private readonly IMetricDefinitionRepository repo;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<MetricDefinitionCommandService> logger;

    private static readonly MetricDefinition[] DEFAULT_METRICS =
    {
        MetricDefinition.Create("Kilometraje", "km"),
        MetricDefinition.Create("Horas de uso", "h"),
        MetricDefinition.Create("Ciclos de trabajo", "ciclos"),
        MetricDefinition.Create("Horas de motor", "h"),
        MetricDefinition.Create("Temperatura", "°C"),
        MetricDefinition.Create("Presión", "bar"),
        MetricDefinition.Create("Vibración", "mm/s")
    };

    public MetricDefinitionCommandService(IMetricDefinitionRepository repo, IUnitOfWork unitOfWork, ILogger<MetricDefinitionCommandService> logger)
    {
        this.repo = repo;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    public async Task Handle(SeedMetricsCommand command)
    {
        foreach (var md in DEFAULT_METRICS)
        {
            if (!await repo.ExistsByNameAsync(md.Name))
            {
                await repo.AddAsync(md);
                logger.LogInformation("Default metric '{Name}' added", md.Name);
            }
        }
        await unitOfWork.CompleteAsync();
    }
} 