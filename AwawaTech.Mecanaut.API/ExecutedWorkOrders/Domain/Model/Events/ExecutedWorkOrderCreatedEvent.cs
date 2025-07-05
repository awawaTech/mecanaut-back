using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Events;

public class ExecutedWorkOrderCreatedEvent : IDomainEvent
{
    public long Id { get; }
    public long TenantId { get; }
    public string Code { get; }

    public ExecutedWorkOrderCreatedEvent(long id, long tenantId, string code)
    {
        Id = id;
        TenantId = tenantId;
        Code = code;
    }
} 