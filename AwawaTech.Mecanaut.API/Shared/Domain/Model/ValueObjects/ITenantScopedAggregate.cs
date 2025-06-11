using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.Shared.Domain.Model;

public interface ITenantScopedAggregate
{
    TenantId Tenant { get; }
}