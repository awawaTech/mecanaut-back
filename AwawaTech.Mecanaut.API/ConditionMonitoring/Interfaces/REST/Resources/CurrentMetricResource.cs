using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;

public record CurrentMetricResource(long MetricId,
                                    string MetricName,
                                    string Unit,
                                    double Value,
                                    DateTime MeasuredAt); 