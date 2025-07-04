using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Infrastructure.Persistence.EFC.Repositories;

public class PlantRepository : BaseRepository<Plant>, IPlantRepository
{
    private readonly AppDbContext _context;
    public PlantRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name, long tenantId)
        => await _context.Plants.AnyAsync(p => p.Name == name && p.TenantId == new TenantId(tenantId));

    public async Task<Plant?> FindByIdAndTenantAsync(long plantId, long tenantId)
        => await _context.Plants.FirstOrDefaultAsync(p => p.Id == plantId && p.TenantId == new TenantId(tenantId));

    public async Task<IEnumerable<Plant>> ListByTenantAsync(long tenantId)
        => await _context.Plants.Where(p => p.TenantId == new TenantId(tenantId)).ToListAsync();

    public async Task<long> CountActiveProductionLinesAsync(long plantId, long tenantId)
        => await _context.ProductionLines.LongCountAsync(pl => pl.PlantId == plantId && pl.TenantId == new TenantId(tenantId) && pl.Status == Domain.Model.ValueObjects.ProductionLineStatus.Running);
} 