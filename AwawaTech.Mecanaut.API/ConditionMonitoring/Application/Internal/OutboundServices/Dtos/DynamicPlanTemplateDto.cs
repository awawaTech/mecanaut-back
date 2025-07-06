namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices.Dtos;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.ValueObjects;

public class DynamicPlanTemplateDto
{
    public string Name { get; set; } // El nombre base del plan dinámico
    public long ProductionLineId { get; set; }
    public WorkOrderType Type { get; set; } = WorkOrderType.Preventive; // por defecto
    public List<string> Tasks { get; set; } = new();
}