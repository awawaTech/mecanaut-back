using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.Competency.Domain.Repositories;

public interface ISkillRepository : IBaseRepository<Skill>
{
    Task<bool> ExistsByNameAsync(string name, long tenantId);
    Task<Skill?> FindByIdAndTenantAsync(long id, long tenantId);
    Task<IEnumerable<Skill>> ListByTenantAsync(long tenantId);
} 