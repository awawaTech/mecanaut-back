using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Transform;

public static class RecordMetricCommandFromResourceAssembler
{
    public static RecordMetricCommand ToCommand(long machineId, RecordMetricResource r)
        => new(machineId, r.MetricId, r.Value, r.MeasuredAt);
} 