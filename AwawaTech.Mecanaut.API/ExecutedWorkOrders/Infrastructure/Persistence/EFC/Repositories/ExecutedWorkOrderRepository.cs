using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Infrastructure.Persistence.EFC.Repositories;

public class ExecutedWorkOrderRepository : BaseRepository<ExecutedWorkOrder>, IExecutedWorkOrderRepository
{
    public ExecutedWorkOrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ExecutedWorkOrder> FindByIdAsync(long id)
    {
        return await Context.ExecutedWorkOrders
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ExecutedWorkOrder>> FindByProductionLineIdAsync(long productionLineId)
    {
        return await Context.ExecutedWorkOrders
            .Where(x => x.ProductionLineId == productionLineId)
            .ToListAsync();
    }

    public async Task AddEntityAsync(UsedProduct entity)
    {
        await Context.UsedProducts.AddAsync(entity);
    }
    
    public async Task AddEntityAsync(ExecutionImages entity)
    {
        await Context.ExecutionImages.AddAsync(entity);
    }
    
    public async Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdAsync(long executedWorkOrderId)
    {
        return await Context.UsedProducts
            .Where(x => x.ExecutedWorkOrderId == executedWorkOrderId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<string>> FindImagesByExecutedWorkOrderIdAsync(long executedWorkOrderId)
    {
        return await Context.ExecutionImages
            .Where(x => x.ExecutedWorkOrderId == executedWorkOrderId)
            .Select(x => x.ImageUrl)
            .ToListAsync();
    }

    public async Task<IEnumerable<UsedProduct>> FindUsedProductsByExecutedWorkOrderIdsAsync(IEnumerable<long> executedWorkOrderIds)
    {
        return await Context.UsedProducts
            .Where(x => executedWorkOrderIds.Contains(x.ExecutedWorkOrderId))
            .ToListAsync();
    }
    
    public async Task<IEnumerable<string>> FindImagesByExecutedWorkOrderIdsAsync(IEnumerable<long> executedWorkOrderIds)
    {
        return await Context.ExecutionImages
            .Where(x => executedWorkOrderIds.Contains(x.ExecutedWorkOrderId))
            .Select(x => x.ImageUrl)
            .ToListAsync();
    }
}