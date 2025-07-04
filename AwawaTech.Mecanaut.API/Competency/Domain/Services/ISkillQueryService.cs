using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.Competency.Domain.Services;

public interface ISkillQueryService
{
    Task<IEnumerable<Skill>> Handle(GetAllSkillsQuery query);
    Task<Skill?> Handle(GetSkillByIdQuery query);
} 