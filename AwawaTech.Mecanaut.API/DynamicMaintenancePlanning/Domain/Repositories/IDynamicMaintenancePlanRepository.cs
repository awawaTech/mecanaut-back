using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Repositories;

public interface IDynamicMaintenancePlanRepository : IBaseRepository<DynamicMaintenancePlan>
{
    Task<bool> ExistsByNameAsync(string name, long tenantId);
    Task<DynamicMaintenancePlan> GetByIdAsync(string id, string tenantId);
    Task<IEnumerable<DynamicMaintenancePlanWithDetails>> GetAllByTenantIdAndPlantLineIdAsync(string tenantId, string plantLineId);
    Task AddEntityAsync<T>(T entity) where T : class;
    
} 