using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Aggregates;

public abstract class MaintenancePlan : AuditableAggregateRoot
{
    public string Name { get; private set; } = null!;
    public PlanPeriod Period { get; private set; }
    public PlanStatus Status { get; private set; }
    public PlanType PlanType { get; private set; }
    public TenantId TenantId { get; private set; }

    protected MaintenancePlan(string name, PlanPeriod period, PlanType type, TenantId tenantId)
    {
        Name      = name ?? throw new ArgumentNullException(nameof(name));
        Period    = period;
        Status    = PlanStatus.Active;
        PlanType  = type;
        TenantId  = tenantId;
    }

    protected MaintenancePlan() { }

    public bool IsActiveOn(DateOnly date) => Status == PlanStatus.Active && Period.Contains(date);

    public void Deactivate() => Status = PlanStatus.Inactive;
} 