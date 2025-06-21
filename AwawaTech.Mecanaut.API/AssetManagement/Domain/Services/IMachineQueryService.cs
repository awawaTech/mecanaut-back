using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IMachineQueryService
{
    Task<IEnumerable<Machine>> Handle(GetAllMachinesQuery query);
    Task<IEnumerable<Machine>> Handle(GetAvailableMachinesQuery query);
    Task<IEnumerable<Machine>> Handle(GetMachinesDueForMaintenanceQuery query);
    Task<IEnumerable<Machine>> Handle(GetMachinesByProductionLineQuery query);
    Task<Machine?> Handle(GetMachineByIdQuery query);
    Task<Machine?> Handle(GetMachineBySerialNumberQuery query);
} 