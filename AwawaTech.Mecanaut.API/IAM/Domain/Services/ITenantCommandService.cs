using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Services;
 
public interface ITenantCommandService
{
    Task<int> Handle(CreateTenantCommand command);
} 