using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

public readonly struct MetricDefinitionId
{
    public long Value { get; }
    public MetricDefinitionId(long value)
    {
        if (value <= 0) throw new ValidationException("MetricDefinitionId must be positive");
        Value = value;
    }
    public override string ToString() => Value.ToString();
} 