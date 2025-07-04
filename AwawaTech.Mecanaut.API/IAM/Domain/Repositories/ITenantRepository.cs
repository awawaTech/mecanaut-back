using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
 
public interface ITenantRepository : IBaseRepository<Tenant>
{
    Task<Tenant?> FindByCodeAsync(string code);
    Task<long> GetSubscriptionPlanIdByTenantId(long tenantId);
} 