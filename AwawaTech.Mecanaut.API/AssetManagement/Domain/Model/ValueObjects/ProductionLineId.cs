namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public readonly record struct ProductionLineId(Guid Value)
{
    public static ProductionLineId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}