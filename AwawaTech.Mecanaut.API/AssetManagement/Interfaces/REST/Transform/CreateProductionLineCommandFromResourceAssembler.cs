using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class CreateProductionLineCommandFromResourceAssembler
{
    public static CreateProductionLineCommand ToCommandFromResource(CreateProductionLineResource resource)
    {
        var capacity = new Capacity(resource.CapacityUnitsPerHour);
        return new CreateProductionLineCommand(resource.Name, resource.Code, capacity, resource.PlantId);
    }
} 