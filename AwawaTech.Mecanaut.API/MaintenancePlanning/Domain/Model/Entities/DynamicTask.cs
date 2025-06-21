using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;
using System.Collections.Generic;
using TaskStatusEnum = AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects.TaskStatus;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Entities;

public class DynamicTask
{
    public long Id { get; private set; }
    public MachineId MachineId { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    private readonly HashSet<SkillId> _requiredSkills = new();
    public IReadOnlyCollection<SkillId> RequiredSkills => _requiredSkills;

    public TaskStatusEnum Status { get; private set; }
    public DateTime? TriggeredAt { get; private set; }

    // Navigation back reference
    public Aggregates.DynamicMaintenancePlan Plan { get; private set; }

    protected DynamicTask() { }

    private DynamicTask(Aggregates.DynamicMaintenancePlan plan,
                       MachineId machineId,
                       string name,
                       string? description,
                       IEnumerable<SkillId>? skills)
    {
        Plan          = plan;
        MachineId     = machineId;
        Name          = name;
        Description   = description;
        if (skills != null) _requiredSkills = new HashSet<SkillId>(skills);
        Status        = TaskStatusEnum.Pending;
    }

    public static DynamicTask Create(Aggregates.DynamicMaintenancePlan plan,
                                     MachineId machineId,
                                     string name,
                                     string? description,
                                     IEnumerable<SkillId>? skills) =>
        new(plan, machineId, name, description, skills);

    public bool CanTrigger(MachineId m) => Status == TaskStatusEnum.Pending && MachineId.Equals(m);

    public void Trigger()
    {
        Status      = TaskStatusEnum.Triggered;
        TriggeredAt = DateTime.UtcNow;
    }
} 