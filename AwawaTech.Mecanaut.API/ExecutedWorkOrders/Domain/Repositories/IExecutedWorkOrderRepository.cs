using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Repositories;

public interface IExecutedWorkOrderRepository : IBaseRepository<ExecutedWorkOrder>
{
    Task<ExecutedWorkOrder> FindByIdAsync(long id);
    Task<IEnumerable<ExecutedWorkOrder>> FindByProductionLineIdAsync(long productionLineId);
    Task AddEntityAsync(UsedProduct entity);
    
    Task AddEntityAsync(ExecutionImages entity);
    Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdAsync(long executedWorkOrderId);

    Task<IEnumerable<string>> FindImagesByExecutedWorkOrderIdAsync(long executedWorkOrderId);

    Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdsAsync(IEnumerable<long> executedWorkOrderIds);
    
    Task<IEnumerable<string>> FindImagesByExecutedWorkOrderIdsAsync(IEnumerable<long> executedWorkOrderIds);
} 