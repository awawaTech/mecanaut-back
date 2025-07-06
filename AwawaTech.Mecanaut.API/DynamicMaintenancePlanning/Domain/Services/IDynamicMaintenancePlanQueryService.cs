using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Services
{
    public interface IDynamicMaintenancePlanQueryService
    {
        Task<DynamicMaintenancePlanWithDetails> GetAsync(GetDynamicMaintenancePlanQuery query);
        Task<IEnumerable<DynamicMaintenancePlanWithDetails>> GetAllAsync(GetAllDynamicMaintenancePlansQuery query);
        Task<DynamicMaintenancePlanWithDetails?> GetByMachineMetricAndAmountAsync(long machineId, long metricId, double amount, string tenantId);
    }
} 