using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Services;
 
public interface IRoleCommandService
{
    Task Handle(SeedRolesCommand command);
} 