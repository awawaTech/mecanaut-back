using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.CommandServices;

public class MachineCommandService : IMachineCommandService
{
    private readonly IMachineRepository machineRepo;
    private readonly IProductionLineRepository lineRepo;
    private readonly IUnitOfWork uow;
    private readonly TenantContextHelper tenantHelper;

    public MachineCommandService(IMachineRepository mr, IProductionLineRepository lr, IUnitOfWork u, TenantContextHelper th)
    {
        machineRepo  = mr;
        lineRepo     = lr;
        uow          = u;
        tenantHelper = th;
    }

    public async Task<Machine> Handle(RegisterMachineCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        if (await machineRepo.ExistsBySerialNumberAsync(command.SerialNumber, tenantId))
            throw new InvalidOperationException("Serial number already exists");

        var machine = Machine.Create(command.SerialNumber, command.Name, command.Specs, command.PlantId, new TenantId(tenantId));
        await machineRepo.AddAsync(machine);
        await uow.CompleteAsync();
        return machine;
    }

    public async Task<Machine> Handle(AssignMachineToProductionLineCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var machine = await machineRepo.FindByIdAndTenantAsync(command.MachineId, tenantId)
                     ?? throw new KeyNotFoundException("Machine not found");
        var line = await lineRepo.FindByIdAndTenantAsync(command.ProductionLineId, tenantId)
                   ?? throw new KeyNotFoundException("Production line not found");

        machine.AssignToProductionLine(line.Id);
        await uow.CompleteAsync();
        return machine;
    }

    public async Task<Machine> Handle(StartMachineMaintenanceCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var machine = await machineRepo.FindByIdAndTenantAsync(command.MachineId, tenantId)
                     ?? throw new KeyNotFoundException("Machine not found");
        machine.StartMaintenance();
        await uow.CompleteAsync();
        return machine;
    }

    public async Task<Machine> Handle(CompleteMachineMaintenanceCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var machine = await machineRepo.FindByIdAndTenantAsync(command.MachineId, tenantId)
                     ?? throw new KeyNotFoundException("Machine not found");
        machine.CompleteMaintenance();
        await uow.CompleteAsync();
        return machine;
    }
} 