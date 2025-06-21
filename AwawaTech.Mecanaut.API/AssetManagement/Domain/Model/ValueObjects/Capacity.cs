using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public readonly struct Capacity
{
    public double UnitsPerHour { get; }

    public Capacity(double unitsPerHour)
    {
        if (unitsPerHour <= 0)
            throw new ValidationException("Units per hour must be positive");
        UnitsPerHour = unitsPerHour;
    }

    public bool IsValid() => UnitsPerHour > 0;

    public override string ToString() => $"{UnitsPerHour} u/h";
} 