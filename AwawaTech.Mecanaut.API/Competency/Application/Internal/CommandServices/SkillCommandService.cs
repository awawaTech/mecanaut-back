using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Competency.Domain.Repositories;
using AwawaTech.Mecanaut.API.Competency.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.Competency.Application.Internal.CommandServices;

public class SkillCommandService : ISkillCommandService
{
    private readonly ISkillRepository skillRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly TenantContextHelper tenantHelper;

    public SkillCommandService(ISkillRepository repo, IUnitOfWork uow, TenantContextHelper helper)
    {
        skillRepository = repo;
        unitOfWork      = uow;
        tenantHelper    = helper;
    }

    public async Task<Skill> Handle(CreateSkillCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        if (await skillRepository.ExistsByNameAsync(command.Name, tenantId))
            throw new InvalidOperationException("Skill name already exists");

        var skill = Skill.Create(command.Name, command.Description, command.Category, new TenantId(tenantId));
        await skillRepository.AddAsync(skill);
        await unitOfWork.CompleteAsync();
        return skill;
    }

    public async Task<Skill> Handle(UpdateSkillCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var skill = await skillRepository.FindByIdAndTenantAsync(command.SkillId, tenantId)
                     ?? throw new KeyNotFoundException("Skill not found");

        if (!skill.Name.Equals(command.Name, StringComparison.OrdinalIgnoreCase) &&
            await skillRepository.ExistsByNameAsync(command.Name, tenantId))
            throw new InvalidOperationException("Skill name already exists");

        skill.Update(command.Name, command.Description, command.Category);
        await unitOfWork.CompleteAsync();
        return skill;
    }

    public async Task<Skill> Handle(DeactivateSkillCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var skill = await skillRepository.FindByIdAndTenantAsync(command.SkillId, tenantId)
                     ?? throw new KeyNotFoundException("Skill not found");
        skill.Deactivate();
        await unitOfWork.CompleteAsync();
        return skill;
    }
} 