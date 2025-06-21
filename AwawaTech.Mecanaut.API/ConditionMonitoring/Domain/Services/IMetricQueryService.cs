using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;

public interface IMetricQueryService
{
    Task<IEnumerable<MetricDefinition>> Handle(GetAllMetricDefinitionsQuery query);
} 