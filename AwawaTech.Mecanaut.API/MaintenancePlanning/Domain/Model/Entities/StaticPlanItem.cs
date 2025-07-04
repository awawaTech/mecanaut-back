using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Entities;

public class StaticPlanItem
{
    public long Id { get; private set; }
    public int DayIndex { get; private set; }

    private readonly List<StaticTask> _tasks = new();
    public IReadOnlyCollection<StaticTask> Tasks => _tasks.AsReadOnly();

    // Navigation property
    public Aggregates.StaticMaintenancePlan Plan { get; private set; }

    internal StaticPlanItem(Aggregates.StaticMaintenancePlan plan, int dayIndex)
    {
        Plan = plan;
        DayIndex = dayIndex;
    }

    protected StaticPlanItem() { }

    public StaticTask AddTask(StaticTask task)
    {
        _tasks.Add(task);
        return task;
    }
} 