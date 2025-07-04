using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Infrastructure.Persistence.EFC.Repositories;

public class ProductionLineRepository : BaseRepository<ProductionLine>, IProductionLineRepository
{
    private readonly AppDbContext _context;
    public ProductionLineRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByCodeAsync(string code, long plantId, long tenantId)
        => await _context.ProductionLines.AnyAsync(pl => pl.Code == code && pl.PlantId == plantId && pl.TenantId == new TenantId(tenantId));

    public async Task<ProductionLine?> FindByIdAndTenantAsync(long id, long tenantId)
        => await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id && pl.TenantId == new TenantId(tenantId));

    public async Task<IEnumerable<ProductionLine>> ListByTenantAsync(long tenantId)
        => await _context.ProductionLines.Where(pl => pl.TenantId == new TenantId(tenantId)).ToListAsync();

    public async Task<IEnumerable<ProductionLine>> ListByPlantAsync(long plantId, long tenantId)
        => await _context.ProductionLines.Where(pl => pl.PlantId == plantId && pl.TenantId == new TenantId(tenantId)).ToListAsync();

    public async Task<IEnumerable<ProductionLine>> ListRunningByTenantAsync(long tenantId)
        => await _context.ProductionLines.Where(pl => pl.TenantId == new TenantId(tenantId) && pl.Status == Domain.Model.ValueObjects.ProductionLineStatus.Running).ToListAsync();
} 