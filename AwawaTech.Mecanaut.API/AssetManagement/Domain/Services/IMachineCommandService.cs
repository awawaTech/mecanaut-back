using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IMachineCommandService
{
    Task<Machine> Handle(RegisterMachineCommand command);
    Task<Machine> Handle(AssignMachineToProductionLineCommand command);
    Task<Machine> Handle(StartMachineMaintenanceCommand command);
    Task<Machine> Handle(CompleteMachineMaintenanceCommand command);
} 