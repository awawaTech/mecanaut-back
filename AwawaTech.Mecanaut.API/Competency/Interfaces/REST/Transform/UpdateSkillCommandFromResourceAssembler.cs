using AwawaTech.Mecanaut.API.Competency.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Transform;

public static class UpdateSkillCommandFromResourceAssembler
{
    public static UpdateSkillCommand ToCommandFromResource(long skillId, UpdateSkillResource resource)
        => new(skillId, resource.Name, resource.Description, resource.Category);
} 