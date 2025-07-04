using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Services;
 
public interface ITenantQueryService
{
    Task<IEnumerable<Tenant>> Handle(GetAllTenantsQuery query);
    Task<Tenant?> Handle(GetTenantByIdQuery query);
} 