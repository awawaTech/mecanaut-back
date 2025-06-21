using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Transform;

public static class MetricDefinitionResourceFromEntityAssembler
{
    public static MetricDefinitionResource ToResource(MetricDefinition entity)
        => new(entity.Id, entity.Name, entity.Unit);
} 