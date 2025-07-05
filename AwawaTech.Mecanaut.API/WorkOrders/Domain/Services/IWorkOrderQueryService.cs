using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Services;

public interface IWorkOrderQueryService
{
    Task<WorkOrder> FindByIdAsync(GetWorkOrderByIdQuery query);
    Task<IEnumerable<WorkOrder>> ListAsync(GetAllWorkOrdersQuery query);
    Task<IEnumerable<WorkOrder>> ListByStatusAsync(GetWorkOrdersByStatusQuery query);
} 