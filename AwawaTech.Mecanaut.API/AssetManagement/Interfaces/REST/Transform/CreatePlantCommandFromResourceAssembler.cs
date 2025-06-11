using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class CreatePlantCommandFromResourceAssembler
{
    public static CreatePlantCommand ToCommand(CreatePlantResource resource)
        => new(resource.Name, resource.Location);
}