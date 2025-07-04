using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Events;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Aggregates;

public class DynamicMaintenancePlan : MaintenancePlan
{
    public MetricDefinitionId MetricId { get; private set; }
    public Threshold Threshold { get; private set; }

    private readonly List<DynamicTask> _tasks = new();
    public IReadOnlyCollection<DynamicTask> Tasks => _tasks.AsReadOnly();

    private DynamicMaintenancePlan(string name,
                                   PlanPeriod period,
                                   MetricDefinitionId metricId,
                                   Threshold threshold,
                                   TenantId tenantId)
        : base(name, period, PlanType.Dynamic, tenantId)
    {
        MetricId  = metricId;
        Threshold = threshold;
        AddDomainEvent(new MaintenancePlanCreatedEvent(Id, tenantId.Value, PlanType.Dynamic));
    }

    public static DynamicMaintenancePlan Create(string name,
                                                PlanPeriod period,
                                                MetricDefinitionId metricId,
                                                Threshold threshold,
                                                TenantId tenantId) =>
        new(name, period, metricId, threshold, tenantId);

    public DynamicTask AddTask(MachineId machineId,
                               string taskName,
                               string? description,
                               IEnumerable<SkillId> skills)
    {
        var task = DynamicTask.Create(this, machineId, taskName, description, skills);
        _tasks.Add(task);
        return task;
    }

    public void EvaluateTrigger(MachineId machineId, double reading)
    {
        if (!IsActiveOn(DateOnly.FromDateTime(DateTime.UtcNow))) return;
        if (reading < Threshold.Value) return;

        foreach (var task in _tasks.Where(t => t.CanTrigger(machineId)))
        {
            task.Trigger();
            AddDomainEvent(new DynamicTaskTriggeredEvent(Id, task.Id, machineId.Value, reading));
        }
    }
} 