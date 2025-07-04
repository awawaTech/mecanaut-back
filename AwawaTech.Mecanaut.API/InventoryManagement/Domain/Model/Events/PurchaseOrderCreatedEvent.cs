using System;
using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Events
{
    public record PurchaseOrderCreatedEvent(
        long Id,
        long InventoryPartId,
        int Quantity
    ) : IDomainEvent;
} 