using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Transform;

public static class RoleResourceFromEntityAssembler
{
    public static RoleResource ToResourceFromEntity(Role role)
        => new(role.Id, role.Name.ToString());
} 