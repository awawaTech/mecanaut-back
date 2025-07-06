namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices.Dtos;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
public interface IDynamicMaintenancePlanningAcl
{
    Task<DynamicPlanTemplateDto> GetPlanTemplateByMetricAsync(long machineId, long metricId, double amount,
        TenantId tenantId);
}