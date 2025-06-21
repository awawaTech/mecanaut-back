using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;

namespace AwawaTech.Mecanaut.API.IAM.Application.Internal.QueryServices;

public class RoleQueryService(IRoleRepository roleRepository) : IRoleQueryService
{
    public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query)
    {
        return await roleRepository.ListAsync();
    }

    public async Task<Role?> Handle(GetRoleByNameQuery query)
    {
        return await roleRepository.FindByNameAsync(query.Name);
    }
} 