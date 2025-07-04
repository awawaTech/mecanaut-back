using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Transform;

public static class CurrentMetricResourceAssembler
{
    public static CurrentMetricResource ToResource(long metricId, CurrentMetric cm, MetricDefinition def)
        => new(metricId, def.Name, def.Unit, cm.Value, cm.MeasuredAt);
} 