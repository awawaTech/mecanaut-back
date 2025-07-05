using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;

public interface IMachineRepository : IBaseRepository<Machine>
{
    Task<bool> ExistsBySerialNumberAsync(string serialNumber, long tenantId);
    Task<Machine?> FindByIdAndTenantAsync(long id, long tenantId);
    Task<Machine?> FindBySerialNumberAndTenantAsync(string serialNumber, long tenantId);
    Task<IEnumerable<Machine>> ListByTenantAsync(long tenantId);
    Task<IEnumerable<Machine>> ListAvailableByTenantAsync(long tenantId);
    Task<IEnumerable<Machine>> ListByProductionLineAsync(long productionLineId, long tenantId);
    Task<IEnumerable<Machine>> ListByPlantIdAsync(long plantId, long tenantId);
    Task<IEnumerable<Machine>> ListMaintenanceDueByTenantAsync(long tenantId);
}