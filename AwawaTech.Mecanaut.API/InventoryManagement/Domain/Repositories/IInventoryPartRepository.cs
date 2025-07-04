using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Repositories
{
    public interface IInventoryPartRepository : IBaseRepository<InventoryPart>
    {
        Task<IEnumerable<InventoryPart>> FindByPlantId(long plantId);
    }
} 