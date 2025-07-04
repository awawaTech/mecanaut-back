using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.QueryServices;

public class ProductionLineQueryService : IProductionLineQueryService
{
    private readonly IProductionLineRepository lineRepo;
    private readonly TenantContextHelper tenantHelper;

    public ProductionLineQueryService(IProductionLineRepository repo, TenantContextHelper helper)
    {
        lineRepo     = repo;
        tenantHelper = helper;
    }

    public async Task<IEnumerable<ProductionLine>> Handle(GetProductionLinesByPlantQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await lineRepo.ListByPlantAsync(query.PlantId, tenantId);
    }

    public async Task<ProductionLine?> Handle(GetProductionLineByIdQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await lineRepo.FindByIdAndTenantAsync(query.ProductionLineId, tenantId);
    }

    public async Task<IEnumerable<ProductionLine>> Handle(GetRunningProductionLinesQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await lineRepo.ListRunningByTenantAsync(tenantId);
    }
} 