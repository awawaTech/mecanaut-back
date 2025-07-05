using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Events;

public class WorkOrderCompletedEvent : IDomainEvent
{
    public long WorkOrderId { get; }
    public long TenantId { get; }

    public WorkOrderCompletedEvent(long workOrderId, long tenantId)
    {
        WorkOrderId = workOrderId;
        TenantId = tenantId;
    }
} 