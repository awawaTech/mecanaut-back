namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Services;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices.Dtos;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Commands;

public class WorkOrderAcl : IWorkOrderAcl
{
    private readonly IWorkOrderCommandService _workOrderCommandService;

    public WorkOrderAcl(IWorkOrderCommandService workOrderCommandService)
    {
        _workOrderCommandService = workOrderCommandService;
    }

    public async Task<long> CreateWorkOrderFromDynamicPlanAsync(CreateWorkOrderFromPlanDto dto)
    {
        var command = new CreateWorkOrderCommand
        {
            Code = dto.Code,
            TenantId = dto.TenantId,
            Date = dto.Date,
            ProductionLineId = dto.ProductionLineId,
            Type = dto.Type,
            MachineIds = dto.MachineIds,
            Tasks = dto.Tasks,
            TechnicianIds = dto.TechnicianIds
        };

        var workOrder = await _workOrderCommandService.Handle(command);
        return workOrder.Id;
    }
}
