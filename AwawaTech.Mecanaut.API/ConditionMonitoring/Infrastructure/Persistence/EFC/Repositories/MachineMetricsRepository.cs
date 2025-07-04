using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Infrastructure.Persistence.EFC.Repositories;

public class MachineMetricsRepository : BaseRepository<MachineMetrics>, IMachineMetricsRepository
{
    public MachineMetricsRepository(AppDbContext context) : base(context) { }

    public async Task<MachineMetrics?> FindByMachineAndTenantAsync(long machineId, long tenantId)
    {
        return await Context.Set<MachineMetrics>()
                            .Include(m => m.Readings)
                            .FirstOrDefaultAsync(m => m.MachineId == machineId &&
                                                     m.TenantId == new TenantId(tenantId));
    }
} 