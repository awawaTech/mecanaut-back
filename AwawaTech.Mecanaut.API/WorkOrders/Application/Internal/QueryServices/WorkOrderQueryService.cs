using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<WorkOrder> Handle(GetWorkOrderByIdQuery query)
    {
        return await _repository.FindByIdAsync(query.Id, query.TenantId);
    }

    public async Task<IEnumerable<WorkOrder>> Handle(GetWorkOrdersByProductionLineQuery query)
    {
        return await _repository.FindByProductionLineAsync(query.ProductionLineId, query.TenantId);
    }
} 