using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Events;

public class MachinesAssignedToWorkOrderEvent : IDomainEvent
{
    public long WorkOrderId { get; }
    public long TenantId { get; }
    public List<long> MachineIds { get; }

    public MachinesAssignedToWorkOrderEvent(long workOrderId, long tenantId, List<long> machineIds)
    {
        WorkOrderId = workOrderId;
        TenantId = tenantId;
        MachineIds = machineIds;
    }
} 