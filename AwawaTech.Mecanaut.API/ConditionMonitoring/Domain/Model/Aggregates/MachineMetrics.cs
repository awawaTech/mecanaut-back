using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Events;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using System;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;

public class MachineMetrics : AuditableAggregateRoot
{
    public long MachineId { get; private set; }
    public TenantId TenantId { get; private set; }

    private readonly Dictionary<long, CurrentMetric> _currentReadings = new();
    private readonly List<MetricReading> _readings = new();

    public IReadOnlyDictionary<long, CurrentMetric> CurrentReadings => _currentReadings;
    public IReadOnlyList<MetricReading> Readings => _readings;

    protected MachineMetrics() { }

    private MachineMetrics(long machineId, TenantId tenantId)
    {
        if (machineId <= 0) throw new ArgumentException("machineId must be positive", nameof(machineId));
        MachineId = machineId;
        TenantId  = tenantId;
    }

    public static MachineMetrics Create(long machineId, TenantId tenantId) => new(machineId, tenantId);

    public void Record(long metricId, double value, DateTime? measuredAt = null)
    {
        if (metricId <= 0) throw new ArgumentException("metricId must be positive", nameof(metricId));
        var timestamp = measuredAt ?? DateTime.UtcNow;

        _readings.Add(new MetricReading(machineId: MachineId,
                                        metricId: metricId,
                                        value: value,
                                        measuredAt: timestamp,
                                        tenantId: TenantId.Value));

        _currentReadings[metricId] = new CurrentMetric(value, timestamp);

        AddDomainEvent(new MetricRecordedEvent(MachineId, metricId, value, timestamp));
    }
} 