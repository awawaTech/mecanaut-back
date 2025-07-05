using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Events;

public class TechniciansAddedToWorkOrderEvent : IDomainEvent
{
    public long WorkOrderId { get; }
    public long TenantId { get; }
    public List<long?> TechnicianIds { get; }

    public TechniciansAddedToWorkOrderEvent(long workOrderId, long tenantId, List<long?> technicianIds)
    {
        WorkOrderId = workOrderId;
        TenantId = tenantId;
        TechnicianIds = technicianIds;
    }
} 