using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

public readonly struct MachineId
{
    public long Value { get; }
    public MachineId(long value)
    {
        if (value <= 0) throw new ValidationException("MachineId must be positive");
        Value = value;
    }
    public override string ToString() => Value.ToString();
} 