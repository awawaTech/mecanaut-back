using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.ValueObjects;

public record CurrentMetric(double Value, DateTime MeasuredAt); 