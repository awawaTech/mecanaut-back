using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities;

public class DynamicMaintenancePlanMachine : AuditableEntity
{
    public long PlanId { get; private set; }
    public long MachineId { get; private set; }

    protected DynamicMaintenancePlanMachine() { }

    public DynamicMaintenancePlanMachine(long planId, long machineId)
    {
        PlanId = planId;
        MachineId = machineId;
    }
} 