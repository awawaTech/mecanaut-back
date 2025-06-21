using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class RoleRepository(AppDbContext context)
    : BaseRepository<Role>(context), IRoleRepository
{
    public async Task<Role?> FindByNameAsync(string name)
    {
        return await context.Set<Role>()
            .FirstOrDefaultAsync(r => r.Name.ToString() == name);
    }
} 