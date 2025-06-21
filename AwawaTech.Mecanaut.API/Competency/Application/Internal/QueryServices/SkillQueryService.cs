using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.Competency.Domain.Repositories;
using AwawaTech.Mecanaut.API.Competency.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.Competency.Application.Internal.QueryServices;

public class SkillQueryService : ISkillQueryService
{
    private readonly ISkillRepository repo;
    private readonly TenantContextHelper tenantHelper;

    public SkillQueryService(ISkillRepository repo, TenantContextHelper helper)
    {
        this.repo = repo;
        tenantHelper = helper;
    }

    public async Task<IEnumerable<Skill>> Handle(GetAllSkillsQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await repo.ListByTenantAsync(tenantId);
    }

    public async Task<Skill?> Handle(GetSkillByIdQuery query)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        return await repo.FindByIdAndTenantAsync(query.SkillId, tenantId);
    }
} 