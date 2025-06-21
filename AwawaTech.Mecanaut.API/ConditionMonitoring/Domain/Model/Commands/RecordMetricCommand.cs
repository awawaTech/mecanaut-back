using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;

public record RecordMetricCommand(long MachineId, long MetricId, double Value, DateTime? MeasuredAt); 