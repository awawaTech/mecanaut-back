using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Services;
 
public interface IExecutedWorkOrderQueryService
{
    Task<ExecutedWorkOrder> FindByIdAsync(long id);
    Task<IEnumerable<ExecutedWorkOrder>> FindByProductionLineIdAsync(long productionLineId);
    Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdAsync(long executedWorkOrderId);

    Task<IEnumerable<string>> FindImagesByExecutedWorkOrderIdAsync(long executedWorkOrderId);
    
    Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdsAsync(IEnumerable<long> executedWorkOrderIds);

    Task<IEnumerable<string>> FindImagesByExecutedWorkOrderIdsAsync(IEnumerable<long> executedWorkOrderIds);
} 