using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class MachineResourceFromEntityAssembler
{
    public static MachineResource ToResourceFromEntity(Machine entity)
    {
        return new MachineResource(
            entity.Id,
            entity.SerialNumber,
            entity.Name,
            entity.Specs.Manufacturer,
            entity.Specs.Model,
            entity.Specs.Type,
            entity.Specs.PowerConsumption,
            entity.Status.ToString(),
            entity.ProductionLineId,
            entity.MaintenanceInfo.LastMaintenance,
            entity.MaintenanceInfo.NextMaintenance);
    }
}