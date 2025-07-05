using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;


namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Infrastructure.Persistence.EFC.Repositories;

public class MachineMetricRelationRepository : BaseRepository<MachineMetricRelation>, IMachineMetricRelationRepository
{
    public MachineMetricRelationRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsAsync(long machineId, long metricDefinitionId, long tenantId)
    {
        return await Context.Set<MachineMetricRelation>()
            .AnyAsync(r => r.MachineId == machineId && r.MetricDefinitionId == metricDefinitionId && r.TenantId == tenantId);
    }

    public async Task<IEnumerable<MachineMetricRelation>> GetByMachineIdAsync(long machineId, long tenantId)
    {
        return await Context.Set<MachineMetricRelation>()
            .Where(r => r.MachineId == machineId && r.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<IEnumerable<MachineMetricRelation>> GetByMetricDefinitionIdAsync(long metricDefinitionId, long tenantId)
    {
        return await Context.Set<MachineMetricRelation>()
            .Where(r => r.MetricDefinitionId == metricDefinitionId && r.TenantId == tenantId)
            .ToListAsync();
    }
}