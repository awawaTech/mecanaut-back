using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Transform;

public static class SkillResourceFromEntityAssembler
{
    public static SkillResource ToResourceFromEntity(Skill entity)
        => new(entity.Id, entity.Name, entity.Description, entity.Category, entity.Status.ToString());
} 