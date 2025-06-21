namespace AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

public readonly record struct TenantId(long Value)
{
    public static readonly TenantId Default = new(1);

    public override string ToString() => Value.ToString();
} 