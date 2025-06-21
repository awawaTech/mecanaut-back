using System;
using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;

public record RecordMetricResource(
    [Required] long MetricId,
    [Required] double Value,
    DateTime? MeasuredAt); 