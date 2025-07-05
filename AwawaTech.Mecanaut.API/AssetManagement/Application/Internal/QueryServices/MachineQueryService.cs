using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.QueryServices;

public class MachineQueryService : IMachineQueryService
{
    private readonly IMachineRepository machineRepo;
    private readonly TenantContextHelper tenantHelper;

    public MachineQueryService(IMachineRepository repo, TenantContextHelper helper)
    {
        machineRepo  = repo;
        tenantHelper = helper;
    }

    public async Task<IEnumerable<Machine>> Handle(GetAllMachinesQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await machineRepo.ListByTenantAsync(tenantId);
    }

    public async Task<IEnumerable<Machine>> Handle(GetAvailableMachinesQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await machineRepo.ListAvailableByTenantAsync(tenantId);
    }

    public async Task<IEnumerable<Machine>> Handle(GetMachinesDueForMaintenanceQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await machineRepo.ListMaintenanceDueByTenantAsync(tenantId);
    }

    public async Task<IEnumerable<Machine>> Handle(GetMachinesByProductionLineQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await machineRepo.ListByProductionLineAsync(query.ProductionLineId, tenantId);
    }
    
    public async Task<IEnumerable<Machine>> Handle(GetMachinesByPlantIdQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await machineRepo.ListByPlantIdAsync(query.PlantId, tenantId);
    }

    public async Task<Machine?> Handle(GetMachineByIdQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await machineRepo.FindByIdAndTenantAsync(query.MachineId, tenantId);
    }

    public async Task<Machine?> Handle(GetMachineBySerialNumberQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await machineRepo.FindBySerialNumberAndTenantAsync(query.SerialNumber, tenantId);
    }
} 