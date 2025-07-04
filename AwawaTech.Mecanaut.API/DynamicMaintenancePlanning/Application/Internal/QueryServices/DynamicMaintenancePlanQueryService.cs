using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Repositories;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities;


namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Application.Internal.QueryServices;

public class DynamicMaintenancePlanQueryService : IDynamicMaintenancePlanQueryService
{
    private readonly IDynamicMaintenancePlanRepository planRepository;
    private readonly TenantContextHelper tenantHelper;

    public DynamicMaintenancePlanQueryService(IDynamicMaintenancePlanRepository repo, TenantContextHelper helper)
    {
        planRepository = repo;
        tenantHelper   = helper;
    }

    public async Task<DynamicMaintenancePlan?> GetAsync(GetDynamicMaintenancePlanQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await planRepository.GetByIdAsync(query.Id, tenantId.ToString());
    }

    public async Task<IEnumerable<DynamicMaintenancePlanWithDetails>> GetAllAsync(GetAllDynamicMaintenancePlansQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await planRepository.GetAllByTenantIdAndPlantLineIdAsync(tenantId.ToString(), query.PlantLineId);
    }
}