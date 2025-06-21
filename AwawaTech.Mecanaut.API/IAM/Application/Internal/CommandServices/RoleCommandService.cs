using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.IAM.Application.Internal.CommandServices;

public class RoleCommandService(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork) : IRoleCommandService
{
    public async Task Handle(SeedRolesCommand command)
    {
        // Default roles from enum
        var defaults = Enum.GetValues<Roles>();
        foreach (var roleName in defaults)
        {
            var nameStr = roleName.ToString();
            if (await roleRepository.FindByNameAsync(nameStr) == null)
            {
                await roleRepository.AddAsync(new Role(roleName));
            }
        }
        await unitOfWork.CompleteAsync();
    }
} 