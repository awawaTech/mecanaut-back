using AwawaTech.Mecanaut.API.Competency.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Transform;

public static class CreateSkillCommandFromResourceAssembler
{
    public static CreateSkillCommand ToCommandFromResource(CreateSkillResource resource)
        => new(resource.Name, resource.Description, resource.Category);
} 