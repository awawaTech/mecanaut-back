using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class TenantRepository(AppDbContext context)
    : BaseRepository<Tenant>(context), ITenantRepository
{
    public async Task<Tenant?> FindByCodeAsync(string code)
    {
        return await context.Set<Tenant>()
            .FirstOrDefaultAsync(t => t.Code == code);
    }
} 