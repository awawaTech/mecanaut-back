using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Infrastructure.Persistence.EFC.Repositories;

public class MachineRepository : BaseRepository<Machine>, IMachineRepository
{
    private readonly AppDbContext _context;
    public MachineRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsBySerialNumberAsync(string serialNumber, long tenantId)
        => await _context.Machines.AnyAsync(m => m.SerialNumber == serialNumber && m.TenantId == new TenantId(tenantId));

    public async Task<Machine?> FindByIdAndTenantAsync(long id, long tenantId)
        => await _context.Machines.FirstOrDefaultAsync(m => m.Id == id && m.TenantId == new TenantId(tenantId));

    public async Task<Machine?> FindBySerialNumberAndTenantAsync(string serialNumber, long tenantId)
        => await _context.Machines.FirstOrDefaultAsync(m => m.SerialNumber == serialNumber && m.TenantId == new TenantId(tenantId));

    public async Task<IEnumerable<Machine>> ListByTenantAsync(long tenantId)
        => await _context.Machines.Where(m => m.TenantId == new TenantId(tenantId)).ToListAsync();

    public async Task<IEnumerable<Machine>> ListAvailableByTenantAsync(long tenantId)
        => await _context.Machines.Where(m => m.TenantId == new TenantId(tenantId) && m.Status == Domain.Model.ValueObjects.MachineStatus.Operational && m.ProductionLineId == null).ToListAsync();

    public async Task<IEnumerable<Machine>> ListByProductionLineAsync(long productionLineId, long tenantId)
        => await _context.Machines.Where(m => m.ProductionLineId == productionLineId && m.TenantId == new TenantId(tenantId)).ToListAsync();

    public async Task<IEnumerable<Machine>> ListByPlantIdAsync(long plantId, long tenantId)
        => await _context.Machines.Where(m => m.PlantId == plantId && m.TenantId == new TenantId(tenantId)).ToListAsync();

    public async Task<IEnumerable<Machine>> ListMaintenanceDueByTenantAsync(long tenantId)
        => await _context.Machines.Where(m => m.TenantId == new TenantId(tenantId) && m.MaintenanceInfo.NextMaintenance <= DateTime.UtcNow).ToListAsync();
} 