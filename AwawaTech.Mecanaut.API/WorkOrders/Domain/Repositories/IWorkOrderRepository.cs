using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Repositories;

public interface IWorkOrderRepository : IBaseRepository<WorkOrder>
{
    Task<WorkOrder> FindByIdAsync(long id, TenantId tenantId);
    Task<WorkOrder> FindByCodeAsync(string code, TenantId tenantId);
    Task<IEnumerable<WorkOrder>> FindByStatusAsync(WorkOrderStatus status, TenantId tenantId, int page, int pageSize);
    Task<bool> ExistsAsync(string code, TenantId tenantId);
    Task<IEnumerable<WorkOrder>> ListAsync(TenantId tenantId, int page, int pageSize);
} 