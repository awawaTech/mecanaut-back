using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices.Dtos;

using Microsoft.Extensions.Logging;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.CommandServices;

public class MachineMetricsCommandService : IMachineMetricsCommandService
{
    private readonly IMachineMetricsRepository metricsRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly TenantContextHelper tenantHelper;
    private readonly IMachineCatalogAcl catalogAcl;
    private readonly ILogger<MachineMetricsCommandService> _logger;
    private readonly IDynamicMaintenancePlanningAcl _planningAcl;
    private readonly IWorkOrderAcl _workOrderAcl;

    public MachineMetricsCommandService(IMachineMetricsRepository metricsRepository,
                                        IUnitOfWork unitOfWork,
                                        TenantContextHelper tenantHelper,
                                        IMachineCatalogAcl catalogAcl,
                                        ILogger<MachineMetricsCommandService> logger,
                                        IDynamicMaintenancePlanningAcl planningAcl,
                                        IWorkOrderAcl workOrderAcl)
    {
        this.metricsRepository = metricsRepository;
        this.unitOfWork = unitOfWork;
        this.tenantHelper = tenantHelper;
        this.catalogAcl = catalogAcl;
        this._planningAcl = planningAcl;
        this._workOrderAcl = workOrderAcl;
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
        
        
        var planTemplate = await _planningAcl.GetPlanTemplateByMetricAsync(
            command.MachineId, command.MetricId, command.Value, new TenantId(tenantId));
        
        if (planTemplate != null)
        {
            var createDto = new CreateWorkOrderFromPlanDto
            {
                Code = $"{planTemplate.Name}_{command.MachineId}",
                TenantId = new TenantId(tenantId),
                Date = DateTime.UtcNow,
                ProductionLineId = planTemplate.ProductionLineId,
                Type = planTemplate.Type,
                MachineIds = new List<long> { command.MachineId },
                Tasks = planTemplate.Tasks,
                TechnicianIds = new List<long?>()
            };

            await _workOrderAcl.CreateWorkOrderFromDynamicPlanAsync(createDto);
        }
        

        await unitOfWork.CompleteAsync();

        _logger.LogDebug("Metric recorded: machine={MachineId}, metric={MetricId}, value={Value}, ts={Timestamp}",
                         command.MachineId, command.MetricId, command.Value, command.MeasuredAt);

        return metrics;
    }
} 