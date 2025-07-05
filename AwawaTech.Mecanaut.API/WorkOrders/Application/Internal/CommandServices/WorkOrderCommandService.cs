using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Repositories;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.WorkOrders.Application.Internal.CommandServices;

public class WorkOrderCommandService : IWorkOrderCommandService
{
    private readonly IWorkOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkOrderCommandService(IWorkOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkOrder> CreateAsync(CreateWorkOrderCommand command)
    {
        var workOrder = WorkOrder.Create(
            command.Code,
            command.TenantId,
            command.Date,
            command.ProductionLineId,
            command.Type
        );

        // Asignar máquinas, técnicos y tareas directamente en la creación
        if (command.MachineIds?.Any() == true)
            workOrder.AssignMachines(command.MachineIds);
        
        if (command.TechnicianIds?.Any() == true)
            workOrder.AddTechnicians(command.TechnicianIds);
        
        if (command.Tasks?.Any() == true)
            workOrder.AddTasks(command.Tasks);

        await _repository.AddAsync(workOrder);
        await _unitOfWork.CompleteAsync();
        
        return workOrder;
    }

    public async Task<WorkOrder> AddTechniciansAsync(AddTechniciansToWorkOrderCommand command)
    {
        var workOrder = await _repository.FindByIdAsync(command.WorkOrderId, command.TenantId);
        workOrder.AddTechnicians(command.TechnicianIds);
        await _unitOfWork.CompleteAsync();
        return workOrder;
    }

    public async Task<WorkOrder> AssignMachinesAsync(AssignMachinesToWorkOrderCommand command)
    {
        var workOrder = await _repository.FindByIdAsync(command.WorkOrderId, command.TenantId);
        workOrder.AssignMachines(command.MachineIds);
        await _unitOfWork.CompleteAsync();
        return workOrder;
    }

    public async Task<WorkOrder> AddTasksAsync(AddTasksToWorkOrderCommand command)
    {
        var workOrder = await _repository.FindByIdAsync(command.WorkOrderId, command.TenantId);
        workOrder.AddTasks(command.Tasks);
        await _unitOfWork.CompleteAsync();
        return workOrder;
    }

    public async Task<WorkOrder> CompleteAsync(CompleteWorkOrderCommand command)
    {
        var workOrder = await _repository.FindByIdAsync(command.WorkOrderId, command.TenantId);
        workOrder.Complete();
        await _unitOfWork.CompleteAsync();
        return workOrder;
    }
} 