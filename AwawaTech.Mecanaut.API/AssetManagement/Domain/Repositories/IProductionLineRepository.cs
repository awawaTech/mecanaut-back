using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;

public interface IProductionLineRepository : IBaseRepository<ProductionLine>
{
    Task<IEnumerable<ProductionLine>> FindByPlantIdAsync(PlantId plantId, CancellationToken ct = default);
}