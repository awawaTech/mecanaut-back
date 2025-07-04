using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Repositories
{
    public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
    {
        Task<IEnumerable<PurchaseOrder>> FindByPlantIdAsync(long plantId);
    }
} 