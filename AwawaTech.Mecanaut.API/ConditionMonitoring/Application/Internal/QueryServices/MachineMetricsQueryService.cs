using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using System.Linq;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.QueryServices;

public class MachineMetricsQueryService : IMachineMetricsQueryService
{
    private readonly IMachineMetricsRepository metricsRepo;
    private readonly IMetricReadingRepository readingRepo;
    private readonly TenantContextHelper tenantHelper;

    public MachineMetricsQueryService(IMachineMetricsRepository metricsRepo,
                                      IMetricReadingRepository readingRepo,
                                      TenantContextHelper tenantHelper)
    {
        this.metricsRepo = metricsRepo;
        this.readingRepo = readingRepo;
        this.tenantHelper = tenantHelper;
    }

    public async Task<IReadOnlyDictionary<long, CurrentMetric>> Handle(GetCurrentMetricsByMachineQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var metrics = await metricsRepo.FindByMachineAndTenantAsync(query.MachineId, tenantId);
        if (metrics is null) return new Dictionary<long, CurrentMetric>();

        if (metrics.CurrentReadings.Any()) return metrics.CurrentReadings;

        // Reconstruir a partir de históricos
        var latest = metrics.Readings
                            .GroupBy(r => r.MetricId)
                            .ToDictionary(g => g.Key,
                                          g => new CurrentMetric(
                                              g.OrderByDescending(r => r.MeasuredAt).First().Value,
                                              g.Max(r => r.MeasuredAt)));
        return latest;
    }

    public async Task<CurrentMetric?> Handle(GetCurrentMetricQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var metrics = await metricsRepo.FindByMachineAndTenantAsync(query.MachineId, tenantId);
        if (metrics is null) return null;

        if (metrics.CurrentReadings.TryGetValue(query.MetricId, out var cm)) return cm;

        var latest = metrics.Readings
                            .Where(r => r.MetricId == query.MetricId)
                            .OrderByDescending(r => r.MeasuredAt)
                            .FirstOrDefault();
        return latest is null ? null : new CurrentMetric(latest.Value, latest.MeasuredAt);
    }

    public async Task<IEnumerable<MetricReading>> Handle(GetMetricReadingsQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await readingRepo.FindByMachineMetricAndDateRangeAsync(
            query.MachineId, query.MetricId, query.From, query.To, query.Page, query.Size);
    }
} 