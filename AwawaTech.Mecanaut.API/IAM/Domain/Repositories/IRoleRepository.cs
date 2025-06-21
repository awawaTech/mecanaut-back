using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> FindByNameAsync(string name);
} 