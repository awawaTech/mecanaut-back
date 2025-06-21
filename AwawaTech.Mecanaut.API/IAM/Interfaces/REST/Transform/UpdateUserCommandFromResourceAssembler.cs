using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Transform;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(int id, UpdateUserResource resource)
    {
        return new UpdateUserCommand(
            id,
            resource.Email,
            resource.FirstName,
            resource.LastName,
            resource.Roles);
    }
} 