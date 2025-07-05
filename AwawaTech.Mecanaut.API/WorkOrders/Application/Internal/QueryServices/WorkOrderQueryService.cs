using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Repositories;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Services;

namespace AwawaTech.Mecanaut.API.WorkOrders.Application.Internal.QueryServices;

public class WorkOrderQueryService : IWorkOrderQueryService
{
    private readonly IWorkOrderRepository _repository;

    public WorkOrderQueryService(IWorkOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorkOrder> FindByIdAsync(GetWorkOrderByIdQuery query)
    {
        return await _repository.FindByIdAsync(query.Id, query.TenantId);
    }

    public async Task<IEnumerable<WorkOrder>> ListAsync(GetAllWorkOrdersQuery query)
    {
        return await _repository.ListAsync(query.TenantId, query.Page, query.PageSize);
    }

    public async Task<IEnumerable<WorkOrder>> ListByStatusAsync(GetWorkOrdersByStatusQuery query)
    {
        return await _repository.FindByStatusAsync(query.Status, query.TenantId, query.Page, query.PageSize);
    }
} 