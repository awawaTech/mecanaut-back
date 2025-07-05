using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Commands;

public class AssignMachinesToWorkOrderCommand
{
    public long WorkOrderId { get; set; }
    public List<long> MachineIds { get; set; }
    public TenantId TenantId { get; set; }
} 