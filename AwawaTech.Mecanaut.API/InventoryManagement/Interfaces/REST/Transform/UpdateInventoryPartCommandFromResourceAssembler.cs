using System;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Transform
{
    public class UpdateInventoryPartCommandFromResourceAssembler
    {
        public UpdateInventoryPartCommand ToCommand(long id, UpdateInventoryPartResource resource)
        {
            return new UpdateInventoryPartCommand(
                id,
                resource.Description,
                resource.CurrentStock,
                resource.MinStock,
                resource.UnitPrice
            );
        }
    }
} 