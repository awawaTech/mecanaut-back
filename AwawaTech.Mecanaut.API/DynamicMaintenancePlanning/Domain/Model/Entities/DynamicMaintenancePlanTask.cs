using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities;

public class DynamicMaintenancePlanTask : AuditableEntity
{
    public long PlanId { get; private set; }
    public string TaskDescription { get; private set; }

    protected DynamicMaintenancePlanTask() { }

    public DynamicMaintenancePlanTask(long planId, string taskDescription)
    {
        PlanId = planId;
        TaskDescription = taskDescription;
    }
} 