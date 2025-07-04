using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.Competency.Domain.Services;

public interface ISkillCommandService
{
    Task<Skill> Handle(CreateSkillCommand command);
    Task<Skill> Handle(UpdateSkillCommand command);
    Task<Skill> Handle(DeactivateSkillCommand command);
} 