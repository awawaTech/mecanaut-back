using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class RegisterMachineCommandFromResourceAssembler
{
    public static RegisterMachineCommand ToCommandFromResource(RegisterMachineResource resource)
        => new(resource.SerialNumber,
               resource.Name,resource.PlantId,
               new MachineSpecs(resource.Manufacturer,
                                resource.Model,
                                resource.Type,
                                resource.PowerConsumption));
} 