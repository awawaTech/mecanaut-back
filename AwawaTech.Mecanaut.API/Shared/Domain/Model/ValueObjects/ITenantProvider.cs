namespace AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

public interface ITenantProvider
{
    TenantId Current { get; }
}