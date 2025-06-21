using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public readonly struct MachineSpecs
{
    public string Brand { get; }
    public string Model { get; }
    public double PowerKw { get; }

    public MachineSpecs(string brand, string model, double powerKw)
    {
        if (string.IsNullOrWhiteSpace(brand)) throw new ValidationException("Brand required");
        if (string.IsNullOrWhiteSpace(model)) throw new ValidationException("Model required");
        if (powerKw <= 0) throw new ValidationException("Power must be positive");
        Brand    = brand;
        Model    = model;
        PowerKw  = powerKw;
    }

    public override string ToString() => $"{Brand} {Model} ({PowerKw} kW)";
} 