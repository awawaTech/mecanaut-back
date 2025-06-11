using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.AssetManagement.Infrastructure.Persistence.EFC.Repositories;

public class ProductionLineRepository(AppDbContext context)
        : BaseRepository<ProductionLine>(context), IProductionLineRepository
{
    public async Task<IEnumerable<ProductionLine>> FindByPlantIdAsync(PlantId plantId, CancellationToken ct = default) =>
        await Context.Lines.Where(l => l.PlantId == plantId).ToListAsync(ct);
}