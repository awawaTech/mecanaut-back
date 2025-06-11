using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.AssetManagement.Infrastructure.Persistence.EFC.Repositories;

public class PlantRepository(AppDbContext context)
        : BaseRepository<Plant>(context), IPlantRepository
{
    public Task<Plant?> FindByIdAsync(PlantId id, CancellationToken ct = default) =>
        Context.Plants.FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task<bool> ExistsNameAsync(TenantId tenant, string name, CancellationToken ct = default) =>
        Context.Plants.AnyAsync(p => p.Tenant == tenant && p.Name == name, ct);
}