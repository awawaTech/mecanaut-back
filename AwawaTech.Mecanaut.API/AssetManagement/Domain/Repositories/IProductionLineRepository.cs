using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;

public interface IProductionLineRepository : IBaseRepository<ProductionLine>
{
    Task<bool> ExistsByCodeAsync(string code, long plantId, long tenantId);
    Task<ProductionLine?> FindByIdAndTenantAsync(long id, long tenantId);
    Task<IEnumerable<ProductionLine>> ListByTenantAsync(long tenantId);
    Task<IEnumerable<ProductionLine>> ListByPlantAsync(long plantId, long tenantId);
    Task<IEnumerable<ProductionLine>> ListRunningByTenantAsync(long tenantId);
} 