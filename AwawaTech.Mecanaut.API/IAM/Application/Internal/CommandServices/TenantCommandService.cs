using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.IAM.Application.Internal.CommandServices;

public class TenantCommandService(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
    : ITenantCommandService
{
    public async Task<int> Handle(CreateTenantCommand command)
    {
        var tenant = new Tenant(
            command.Ruc,
            command.LegalName,
            command.CommercialName,
            command.Address,
            command.City,
            command.Country,
            string.IsNullOrWhiteSpace(command.Phone) ? null : new PhoneNumber(command.Phone),
            string.IsNullOrWhiteSpace(command.Email) ? null : new EmailAddress(command.Email),
            command.Website);
        await tenantRepository.AddAsync(tenant);
        await unitOfWork.CompleteAsync();
        return (int)tenant.Id;
    }
} 