using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class AssignMachineCommandFromResourceAssembler
{
    public static AssignMachineToProductionLineCommand ToCommandFromResource(long machineId, AssignMachineResource resource)
        => new(machineId, resource.ProductionLineId);
}