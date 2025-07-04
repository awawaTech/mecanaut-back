using System;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands
{
    public record CreatePurchaseOrderCommand(
        string OrderNumber,
        long InventoryPartId,
        int Quantity,
        decimal TotalPrice,
        long PlantId,
        DateTime? DeliveryDate = null
    );
} 