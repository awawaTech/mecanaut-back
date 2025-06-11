using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;

public interface IPlantRepository : IBaseRepository<Plant>
{
    Task<Plant?> FindByIdAsync(PlantId id, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(TenantId tenant, string name, CancellationToken ct = default);
}