using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Services;

public interface IRoleQueryService
{
    Task<IEnumerable<Role>> Handle(GetAllRolesQuery query);
    Task<Role?> Handle(GetRoleByNameQuery query);
} 