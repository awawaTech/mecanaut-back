using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Entities;

public class StaticTask
{
    public long Id { get; private set; }
    public MachineId MachineId { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    private readonly HashSet<SkillId> _requiredSkills = new();
    public IReadOnlyCollection<SkillId> RequiredSkills => _requiredSkills;

    protected StaticTask() { }

    public StaticTask(MachineId machineId, string name, string? description, IEnumerable<SkillId>? skills)
    {
        MachineId     = machineId;
        Name          = name;
        Description   = description;
        if (skills != null) _requiredSkills = new HashSet<SkillId>(skills);
    }
} 