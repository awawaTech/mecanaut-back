using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Repositories;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Services;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Application.Internal.QueryServices;

public class ExecutedWorkOrderQueryService : IExecutedWorkOrderQueryService
{
    private readonly IExecutedWorkOrderRepository _repository;

    public ExecutedWorkOrderQueryService(IExecutedWorkOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExecutedWorkOrder> FindByIdAsync(long id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<ExecutedWorkOrder>> FindByProductionLineIdAsync(long productionLineId)
    {
        return await _repository.FindByProductionLineIdAsync(productionLineId);
    }

    public async Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdAsync(long executedWorkOrderId)
    {
        return await _repository.FindUsedProductsByExecutedWorkOrderIdAsync(executedWorkOrderId);
    }

    public async Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdsAsync(IEnumerable<long> executedWorkOrderIds)
    {
        return await _repository.FindUsedProductsByExecutedWorkOrderIdsAsync(executedWorkOrderIds);
    }
} 