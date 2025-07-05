using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Events;

public class TasksAddedToWorkOrderEvent : IDomainEvent
{
    public long WorkOrderId { get; }
    public long TenantId { get; }
    public List<string> Tasks { get; }

    public TasksAddedToWorkOrderEvent(long workOrderId, long tenantId, List<string> tasks)
    {
        WorkOrderId = workOrderId;
        TenantId = tenantId;
        Tasks = tasks;
    }
} 