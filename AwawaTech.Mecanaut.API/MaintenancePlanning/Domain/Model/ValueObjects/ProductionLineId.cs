using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

public readonly struct ProductionLineId
{
    public long Value { get; }

    public ProductionLineId(long value)
    {
        if (value <= 0)
            throw new ValidationException("ProductionLineId must be positive");
        Value = value;
    }

    public override string ToString() => Value.ToString();
} 