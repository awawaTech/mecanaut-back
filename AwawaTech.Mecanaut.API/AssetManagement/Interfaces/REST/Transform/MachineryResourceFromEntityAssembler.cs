using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class MachineryResourceFromEntityAssembler
{
    public static MachineryResource ToResource(Machinery entity)
        => new(entity.Id.Value, entity.Model, entity.Brand);
}
