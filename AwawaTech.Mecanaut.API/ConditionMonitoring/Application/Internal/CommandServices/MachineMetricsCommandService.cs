using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using Microsoft.Extensions.Logging;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.CommandServices;

public class MachineMetricsCommandService : IMachineMetricsCommandService
{
    private readonly IMachineMetricsRepository metricsRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly TenantContextHelper tenantHelper;
    private readonly IMachineCatalogAcl catalogAcl;
    private readonly ILogger<MachineMetricsCommandService> _logger;

    public MachineMetricsCommandService(IMachineMetricsRepository metricsRepository,
                                        IUnitOfWork unitOfWork,
                                        TenantContextHelper tenantHelper,
                                        IMachineCatalogAcl catalogAcl,
                                        ILogger<MachineMetricsCommandService> logger)
    {
        this.metricsRepository = metricsRepository;
        this.unitOfWork = unitOfWork;
        this.tenantHelper = tenantHelper;
        this.catalogAcl = catalogAcl;
        _logger = logger;
    }

    public async Task<MachineMetrics> Handle(RecordMetricCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        if (tenantId == 0) throw new InvalidOperationException("Tenant context missing");

        // ACL check
        if (!catalogAcl.MachineExists(command.MachineId, tenantId))
            throw new ArgumentException("Machine ID not found in AssetManagement");

        // Obtener o crear agregado
        var metrics = await metricsRepository.FindByMachineAndTenantAsync(command.MachineId, tenantId);
        bool isNew = metrics == null;
        if (isNew)
            metrics = MachineMetrics.Create(command.MachineId, new TenantId(tenantId));

        metrics!.Record(command.MetricId, command.Value, command.MeasuredAt);

        if (isNew)
            await metricsRepository.AddAsync(metrics);
        else
            metricsRepository.Update(metrics);

        await unitOfWork.CompleteAsync();

        _logger.LogDebug("Metric recorded: machine={MachineId}, metric={MetricId}, value={Value}, ts={Timestamp}",
                         command.MachineId, command.MetricId, command.Value, command.MeasuredAt);

        return metrics;
    }
} 