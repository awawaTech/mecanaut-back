using System.Collections.Generic;
using System.Linq;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Transform;

public class InventoryPartResourceAssembler : IInventoryPartResourceAssembler
{
    public InventoryPartResource ToResource(InventoryPart entity)
    {
        return new InventoryPartResource
        {
            Id = entity.Id,
            Code = entity.PartNumber,
            Name = entity.Name,
            Description = entity.Description,
            CurrentStock = entity.CurrentStock,
            MinStock = entity.MinimumStock,
            UnitPrice = entity.UnitPrice.Amount
        };
    }

    public IEnumerable<InventoryPartResource> ToResource(IEnumerable<InventoryPart> entities)
    {
        return entities.Select(ToResource);
    }
} 