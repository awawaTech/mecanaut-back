namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public readonly record struct MachineryId(Guid Value)
{
    public static MachineryId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}