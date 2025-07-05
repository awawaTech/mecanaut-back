using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Repositories;

public interface IWorkOrderRepository : IBaseRepository<WorkOrder>
{
    Task<WorkOrder> FindByIdAsync(long id, TenantId tenantId);
    Task<IEnumerable<WorkOrder>> FindByProductionLineAsync(long productionLineId, TenantId tenantId);
} 