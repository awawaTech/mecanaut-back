using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Events;

public class WorkOrderCreatedEvent : IDomainEvent
{
    public long WorkOrderId { get; }
    public long TenantId { get; }
    public string Code { get; }

    public WorkOrderCreatedEvent(long workOrderId, long tenantId, string code)
    {
        WorkOrderId = workOrderId;
        TenantId = tenantId;
        Code = code;
    }
} 