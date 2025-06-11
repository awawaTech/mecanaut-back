using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.QueryServices;

public class ProductionLineQueryService(IProductionLineRepository lines)
    : IProductionLineQueryService
{
    public async Task<IEnumerable<ProductionLine>> Handle(GetProductionLinesByPlantQuery query, CancellationToken ct = default)
        => await lines.FindByPlantIdAsync(query.PlantId, ct);
}