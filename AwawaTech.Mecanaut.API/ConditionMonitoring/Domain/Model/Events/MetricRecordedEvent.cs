using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Events;

public record MetricRecordedEvent(long MachineId, long MetricId, double Value, DateTime MeasuredAt)
    : IDomainEvent; 