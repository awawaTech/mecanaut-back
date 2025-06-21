using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Queries;

public record GetMetricReadingsQuery(long MachineId, long MetricId, DateTime? From, DateTime? To, int Page, int Size); 