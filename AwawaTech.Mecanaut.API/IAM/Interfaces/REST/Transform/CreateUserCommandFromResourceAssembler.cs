using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        return new CreateUserCommand(
            resource.Username,
            resource.Password,
            resource.Email,
            resource.FirstName,
            resource.LastName,
            resource.Roles);
    }
} 