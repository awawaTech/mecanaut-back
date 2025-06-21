using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;

public interface IPlantRepository : IBaseRepository<Plant>
{
    Task<bool> ExistsByNameAsync(string name, long tenantId);
    Task<Plant?> FindByIdAndTenantAsync(long plantId, long tenantId);
    Task<IEnumerable<Plant>> ListByTenantAsync(long tenantId);
    Task<long> CountActiveProductionLinesAsync(long plantId, long tenantId);
} 