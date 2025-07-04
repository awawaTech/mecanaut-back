using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Transform;

public static class MetricReadingResourceFromEntityAssembler
{
    public static MetricReadingResource ToResource(MetricReading e)
        => new(e.Id, e.Value, e.MeasuredAt);
} 