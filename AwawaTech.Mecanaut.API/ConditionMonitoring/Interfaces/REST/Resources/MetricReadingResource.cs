using System;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;

public record MetricReadingResource(long ReadingId,
                                    double Value,
                                    DateTime MeasuredAt); 