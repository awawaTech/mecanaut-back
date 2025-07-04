using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.QueryServices;

public class PlantQueryService : IPlantQueryService
{
    private readonly IPlantRepository plantRepo;
    private readonly TenantContextHelper tenantHelper;

    public PlantQueryService(IPlantRepository repo, TenantContextHelper helper)
    {
        plantRepo    = repo;
        tenantHelper = helper;
    }

    public async Task<IEnumerable<Plant>> Handle(GetAllPlantsQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await plantRepo.ListByTenantAsync(tenantId);
    }

    public async Task<Plant?> Handle(GetPlantByIdQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await plantRepo.FindByIdAndTenantAsync(query.PlantId, tenantId);
    }
} 