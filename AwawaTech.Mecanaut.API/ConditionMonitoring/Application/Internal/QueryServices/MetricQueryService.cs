using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.QueryServices;

public class MetricQueryService : IMetricQueryService
{
    private readonly IMetricDefinitionRepository repo;

    public MetricQueryService(IMetricDefinitionRepository repo)
    {
        this.repo = repo;
    }

    public async Task<IEnumerable<MetricDefinition>> Handle(GetAllMetricDefinitionsQuery query)
        => await repo.ListAsync();
} 