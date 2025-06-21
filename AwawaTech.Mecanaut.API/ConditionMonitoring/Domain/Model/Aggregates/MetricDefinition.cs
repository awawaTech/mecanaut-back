using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Events;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;

public class MetricDefinition : AuditableAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string Unit { get; private set; } = null!;

    protected MetricDefinition() { }

    private MetricDefinition(string name, string unit)
    {
        Name = name;
        Unit = unit;
        AddDomainEvent(new MetricDefinitionCreatedEvent(Id, name, unit));
    }

    public static MetricDefinition Create(string name, string unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Metric name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Metric unit is required", nameof(unit));
        return new MetricDefinition(name.Trim(), unit.Trim());
    }
} 