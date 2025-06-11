namespace AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Tenant identifier value object
/// </summary>
/// <param name="Value">
///     The tenant identifier
/// </param>
public readonly record struct TenantId(Guid Value)
{
    public static TenantId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}
