using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.QueryServices;

public class PlantQueryService(IPlantRepository plants, ITenantProvider tenantProvider)
    : IPlantQueryService
{
    public async Task<IEnumerable<Plant>> Handle(GetAllPlantsQuery query, CancellationToken ct = default)
        => await plants.ListAsync();

    public async Task<Plant?> Handle(GetPlantByIdQuery query, CancellationToken ct = default)
        => await plants.FindByIdAsync(query.PlantId, ct);
}