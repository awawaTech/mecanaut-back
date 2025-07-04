using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;

public class MetricReading : AuditableEntity
{
    public long MachineId { get; private set; }
    public long MetricId { get; private set; }
    public double Value { get; private set; }
    public DateTime MeasuredAt { get; private set; }
    public long TenantId { get; private set; }

    protected MetricReading() { }

    public MetricReading(long machineId, long metricId, double value, DateTime measuredAt, long tenantId)
    {
        MachineId  = machineId;
        MetricId   = metricId;
        Value      = value;
        MeasuredAt = measuredAt;
        TenantId   = tenantId;
    }
} 