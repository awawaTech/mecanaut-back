using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Events
{
    public record InventoryPartStockUpdatedEvent(long Id, int NewQuantity) : IDomainEvent;
} 