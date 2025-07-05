namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Application.Internal.OutboundServices;

public interface IInventoryManagementAcl
{
    Task DecreaseInventoryQuantityAsync(long inventoryPartId, int quantity);
}