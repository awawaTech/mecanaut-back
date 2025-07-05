using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Commands;

public class CreateWorkOrderCommand
{
    public string Code { get; set; }
    public long ProductionLineId { get; set; }
    public WorkOrderType Type { get; set; }
    public DateTime Date { get; set; }
    public TenantId TenantId { get; set; }
    public List<long> MachineIds { get; set; }
    public List<long?> TechnicianIds { get; set; }
    public List<string> Tasks { get; set; }
} 