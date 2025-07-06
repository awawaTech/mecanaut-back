namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Repositories;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices.Dtos;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

public class DynamicMaintenancePlanningAcl : IDynamicMaintenancePlanningAcl
{
    private readonly IDynamicMaintenancePlanQueryService _queryService;

    public DynamicMaintenancePlanningAcl(IDynamicMaintenancePlanQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<DynamicPlanTemplateDto> GetPlanTemplateByMetricAsync(long machineId, long metricId, double amount, TenantId tenantId)
    {
        var planWithDetails = await _queryService.GetByMachineMetricAndAmountAsync(machineId, metricId, amount, tenantId.Value.ToString());
        if (planWithDetails == null) return null;

        return new DynamicPlanTemplateDto
        {
            Name = planWithDetails.Plan.Name,
            ProductionLineId = planWithDetails.Plan.ProductionLineId,
            Type = WorkOrderType.Preventive,
            Tasks = planWithDetails.Tasks.Select(t => t.TaskDescription).ToList()
        };
    }
}
