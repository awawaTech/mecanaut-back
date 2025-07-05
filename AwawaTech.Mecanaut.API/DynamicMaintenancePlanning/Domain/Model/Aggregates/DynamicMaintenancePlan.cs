using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Events;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;

public class DynamicMaintenancePlan : AuditableAggregateRoot
{
    public string Name { get; private set; }
    public long MetricId { get; private set; }
    
    public long Amount { get; private set; }
    
    public long ProductionLineId { get; private set; }
    
    public long PlantLineId { get; private set; }
    public TenantId TenantId { get; private set; }
    public PlanStatus Status { get; private set; }

    protected DynamicMaintenancePlan() { }

    private DynamicMaintenancePlan(string name, long metricId, long amount, long productionLineId, long plantLineId, TenantId tenantId)
    {
        Name = name;
        MetricId = metricId;
        Amount = amount;
        ProductionLineId = productionLineId;
        PlantLineId = plantLineId;
        TenantId = tenantId;
        Status = PlanStatus.Active;
        
        AddDomainEvent(new DynamicMaintenancePlanCreatedEvent(Id, tenantId.Value, name)); // Incluir Amount en el evento
    }

    public static DynamicMaintenancePlan Create(string name, long metricId, long amount, long productionLineId, long plantLineId, TenantId tenantId)
    {
        return new DynamicMaintenancePlan(name, metricId, amount, productionLineId, plantLineId, tenantId);
    }

    public void Activate()
    {
        if (Status != PlanStatus.Active)
        {
            Status = PlanStatus.Active;
            // Aquí podríamos agregar un evento de activación si fuera necesario
        }
    }

    public void Deactivate()
    {
        if (Status != PlanStatus.Inactive)
        {
            Status = PlanStatus.Inactive;
            // Aquí podríamos agregar un evento de desactivación si fuera necesario
        }
    }
} 