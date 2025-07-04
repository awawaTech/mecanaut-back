using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Events;

public record PurchaseOrderApprovedEvent(long Id) : IDomainEvent;
public record PurchaseOrderCompletedEvent(long Id) : IDomainEvent;
public record PurchaseOrderCancelledEvent(long Id, string Reason) : IDomainEvent;