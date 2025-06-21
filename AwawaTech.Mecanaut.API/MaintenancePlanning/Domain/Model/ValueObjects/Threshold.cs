using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

public readonly struct Threshold
{
    public double Value { get; }
    public Threshold(double value)
    {
        if (double.IsNaN(value)) throw new ValidationException("Threshold value required");
        Value = value;
    }
    public override string ToString() => Value.ToString("F2");
} 