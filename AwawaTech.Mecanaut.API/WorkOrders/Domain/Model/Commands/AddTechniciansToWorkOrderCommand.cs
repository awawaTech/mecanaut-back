using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Commands;

public class AddTechniciansToWorkOrderCommand
{
    public long WorkOrderId { get; set; }
    public List<long?> TechnicianIds { get; set; }
    public TenantId TenantId { get; set; }
} 