using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class LineResourceFromEntityAssembler
{
    public static ProductionLineResource ToResource(ProductionLine entity)
        => new(entity.Id.Value, entity.Name, entity.Capacity, entity.Machinery.Count);
}
