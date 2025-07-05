using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.WorkOrders.Infrastructure.Persistence.EFC.Repositories;

public class WorkOrderRepository : BaseRepository<WorkOrder>, IWorkOrderRepository
{
    public WorkOrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<WorkOrder> FindByIdAsync(long id, TenantId tenantId)
    {
        return await Context.WorkOrders
            .FirstOrDefaultAsync(x => x.Id == id && x.TenantId.Value == tenantId.Value);
    }

    public async Task<WorkOrder> FindByCodeAsync(string code, TenantId tenantId)
    {
        return await Context.WorkOrders
            .FirstOrDefaultAsync(x => x.Code == code && x.TenantId.Value == tenantId.Value);
    }

    public async Task<IEnumerable<WorkOrder>> FindByStatusAsync(WorkOrderStatus status, TenantId tenantId, int page, int pageSize)
    {
        return await Context.WorkOrders
            .Where(x => x.Status == status && x.TenantId.Value == tenantId.Value)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(string code, TenantId tenantId)
    {
        return await Context.WorkOrders
            .AnyAsync(x => x.Code == code && x.TenantId.Value == tenantId.Value);
    }

    public new async Task<IEnumerable<WorkOrder>> ListAsync(TenantId tenantId, int page, int pageSize)
    {
        return await Context.WorkOrders
            .Where(x => x.TenantId.Value == tenantId.Value)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
} 