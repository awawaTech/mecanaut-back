using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Infrastructure.Persistence.EFC.Repositories;

public class MetricReadingRepository : BaseRepository<MetricReading>, IMetricReadingRepository
{
    public MetricReadingRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<MetricReading>> FindByMachineMetricAndDateRangeAsync(long machineId, long metricId, DateTime? from, DateTime? to, int page, int size)
    {
        var query = Context.Set<MetricReading>().AsQueryable();
        query = query.Where(r => r.MachineId == machineId && r.MetricId == metricId);
        if (from.HasValue) query = query.Where(r => r.MeasuredAt >= from.Value);
        if (to.HasValue)   query = query.Where(r => r.MeasuredAt <= to.Value);
        return await query.OrderByDescending(r => r.MeasuredAt)
                          .Skip(page * size)
                          .Take(size)
                          .ToListAsync();
    }
} 