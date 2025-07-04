using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;

public interface IMachineMetricsQueryService
{
    Task<IReadOnlyDictionary<long, CurrentMetric>> Handle(GetCurrentMetricsByMachineQuery query);
    Task<CurrentMetric?> Handle(GetCurrentMetricQuery query);
    Task<IEnumerable<MetricReading>> Handle(GetMetricReadingsQuery query);
} 