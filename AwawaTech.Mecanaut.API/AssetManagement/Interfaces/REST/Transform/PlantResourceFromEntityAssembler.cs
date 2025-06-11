using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class PlantResourceFromEntityAssembler
{
    public static PlantResource ToResource(Plant entity)
        => new(entity.Id.Value, entity.Name, entity.Location, entity.Lines.Count);
}
