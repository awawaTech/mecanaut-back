using System.Collections.Generic;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Transform;

public interface IPurchaseOrderResourceAssembler
{
    PurchaseOrderResource ToResource(PurchaseOrder entity);
    IEnumerable<PurchaseOrderResource> ToResource(IEnumerable<PurchaseOrder> entities);
} 