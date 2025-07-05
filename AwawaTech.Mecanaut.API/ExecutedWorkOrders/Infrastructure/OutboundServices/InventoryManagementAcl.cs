using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Application.Internal.OutboundServices;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Infrastructure.OutboundServices;

public class InventoryManagementAcl : IInventoryManagementAcl
{
    private readonly IInventoryPartCommandService _inventoryPartCommandService;

    public InventoryManagementAcl(IInventoryPartCommandService inventoryPartCommandService)
    {
        _inventoryPartCommandService = inventoryPartCommandService;
    }

    public async Task DecreaseInventoryQuantityAsync(long inventoryPartId, int quantity)
    {
        var command = new DecreaseInventoryCommand
        {
            InventoryPartId = inventoryPartId,
            Quantity = quantity
        };

        await _inventoryPartCommandService.HandleAsync(command);
    }
}