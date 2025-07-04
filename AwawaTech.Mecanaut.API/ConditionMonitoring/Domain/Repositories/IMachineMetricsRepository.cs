using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;

public interface IMachineMetricsRepository : IBaseRepository<MachineMetrics>
{
    Task<MachineMetrics?> FindByMachineAndTenantAsync(long machineId, long tenantId);
} 