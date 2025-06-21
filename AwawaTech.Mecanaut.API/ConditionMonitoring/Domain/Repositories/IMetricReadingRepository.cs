using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;

public interface IMetricReadingRepository : IBaseRepository<MetricReading>
{
    Task<IEnumerable<MetricReading>> FindByMachineMetricAndDateRangeAsync(long machineId, long metricId, DateTime? from, DateTime? to, int page, int size);
} 