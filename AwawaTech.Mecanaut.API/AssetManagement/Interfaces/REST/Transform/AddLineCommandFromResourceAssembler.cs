using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class AddLineCommandFromResourceAssembler
{
    public static AddProductionLineCommand ToCommand(Guid plantId, AddLineResource resource)
        => new(new PlantId(plantId), resource.Name, resource.Capacity);
}