using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class ProductionLineResourceFromEntityAssembler
{
    public static ProductionLineResource ToResourceFromEntity(ProductionLine entity)
        => new(entity.Id, entity.Name, entity.Code, entity.Capacity.UnitsPerHour, entity.Status.ToString(), entity.PlantId);
} 