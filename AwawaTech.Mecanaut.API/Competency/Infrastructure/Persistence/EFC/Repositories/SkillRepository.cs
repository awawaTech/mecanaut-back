using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Competency.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.Competency.Infrastructure.Persistence.EFC.Repositories;

public class SkillRepository : BaseRepository<Skill>, ISkillRepository
{
    private readonly AppDbContext _context;

    public SkillRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name, long tenantId)
        => await _context.Set<Skill>().AnyAsync(s => s.Name == name && s.TenantId == new TenantId(tenantId));

    public async Task<Skill?> FindByIdAndTenantAsync(long id, long tenantId)
        => await _context.Set<Skill>().FirstOrDefaultAsync(s => s.Id == id && s.TenantId == new TenantId(tenantId));

    public async Task<IEnumerable<Skill>> ListByTenantAsync(long tenantId)
        => await _context.Set<Skill>().Where(s => s.TenantId == new TenantId(tenantId)).ToListAsync();
} 