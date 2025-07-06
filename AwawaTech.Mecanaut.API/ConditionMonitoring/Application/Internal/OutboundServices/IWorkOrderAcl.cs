namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices.Dtos;

public interface IWorkOrderAcl
{
    Task<long> CreateWorkOrderFromDynamicPlanAsync(CreateWorkOrderFromPlanDto dto);
}