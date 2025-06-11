namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public readonly record struct PlantId(Guid Value)
{
    public static PlantId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}