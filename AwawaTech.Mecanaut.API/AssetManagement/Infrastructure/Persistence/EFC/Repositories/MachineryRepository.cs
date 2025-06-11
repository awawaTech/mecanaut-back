using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.AssetManagement.Infrastructure.Persistence.EFC.Repositories;

public class MachineryRepository(AppDbContext  context)
        : BaseRepository<Machinery>(context), IMachineryRepository
{
    public async Task<IEnumerable<Machinery>> FindByLineIdAsync(ProductionLineId lineId, CancellationToken ct = default) =>
        await Context.Machineries.Where(m => m.LineId == lineId).ToListAsync(ct);
}