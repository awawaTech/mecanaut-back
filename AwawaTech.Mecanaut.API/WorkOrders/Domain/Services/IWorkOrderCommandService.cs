using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Services;

public interface IWorkOrderCommandService
{
    Task<WorkOrder> CreateAsync(CreateWorkOrderCommand command);
    Task<WorkOrder> AddTechniciansAsync(AddTechniciansToWorkOrderCommand command);
    Task<WorkOrder> AssignMachinesAsync(AssignMachinesToWorkOrderCommand command);
    Task<WorkOrder> AddTasksAsync(AddTasksToWorkOrderCommand command);
    Task<WorkOrder> CompleteAsync(CompleteWorkOrderCommand command);
} 