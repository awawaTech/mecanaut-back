using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories
{
    public class InventoryPartRepository : BaseRepository<InventoryPart>, IInventoryPartRepository
    {
        public InventoryPartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<InventoryPart>> FindByPlantId(long plantId)
        {
            return await Context.Set<InventoryPart>()
                .Where(x => x.PlantId == plantId)
                .ToListAsync();
        }
    }
} 