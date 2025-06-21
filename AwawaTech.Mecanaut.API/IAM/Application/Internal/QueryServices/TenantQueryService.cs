using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;

namespace AwawaTech.Mecanaut.API.IAM.Application.Internal.QueryServices;

public class TenantQueryService(ITenantRepository tenantRepository) : ITenantQueryService
{
    public async Task<IEnumerable<Tenant>> Handle(GetAllTenantsQuery query)
    {
        return await tenantRepository.ListAsync();
    }

    public async Task<Tenant?> Handle(GetTenantByIdQuery query)
    {
        return await tenantRepository.FindByIdAsync(query.Id);
    }
} 