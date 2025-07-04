using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public sealed class MachineSpecs
{
    public string Manufacturer     { get; private set; } = null!;
    public string Model            { get; private set; } = null!;
    public string Type             { get; private set; } = null!;
    public double PowerConsumption { get; private set; }

    public MachineSpecs(string manufacturer, string model, string type, double powerConsumption)
    {
        if (string.IsNullOrWhiteSpace(manufacturer)) throw new ValidationException("Manufacturer required");
        if (string.IsNullOrWhiteSpace(model)) throw new ValidationException("Model required");
        if (string.IsNullOrWhiteSpace(type))  throw new ValidationException("Type required");
        if (powerConsumption <= 0) throw new ValidationException("Power must be positive");
        Manufacturer     = manufacturer;
        Model            = model;
        Type             = type;
        PowerConsumption = powerConsumption;
    }

    protected MachineSpecs() { }

    public override string ToString() => $"{Manufacturer} {Model} {Type} ({PowerConsumption} kW)";
} 