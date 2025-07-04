using System.Collections.Generic;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Transform;

public interface IInventoryPartResourceAssembler
{
    InventoryPartResource ToResource(InventoryPart entity);
    IEnumerable<InventoryPartResource> ToResource(IEnumerable<InventoryPart> entities);
} 