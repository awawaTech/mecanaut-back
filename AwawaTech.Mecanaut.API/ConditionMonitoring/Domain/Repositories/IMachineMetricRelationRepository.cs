using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;

public interface IMachineMetricRelationRepository : IBaseRepository<MachineMetricRelation>
{
    Task<bool> ExistsAsync(long machineId, long metricDefinitionId, long tenantId);
    Task<IEnumerable<MachineMetricRelation>> GetByMachineIdAsync(long machineId, long tenantId);
    Task<IEnumerable<MachineMetricRelation>> GetByMetricDefinitionIdAsync(long metricDefinitionId, long tenantId);
}