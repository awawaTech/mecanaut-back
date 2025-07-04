using System;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;
using AwawaTech.Mecanaut.API.IAM.Interfaces.ACL;
using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
namespace AwawaTech.Mecanaut.API.IAM.Application.Internal.CommandServices;

public class TenantCommandService : ITenantCommandService
{
    private readonly ITenantRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _eventDispatcher;
    private readonly ISubscriptionPlanAcl _subscriptionPlanAcl;

    public TenantCommandService(
        ITenantRepository repository,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher eventDispatcher,
        ISubscriptionPlanAcl subscriptionPlanAcl)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
        _subscriptionPlanAcl = subscriptionPlanAcl;
    }

    public async Task<int> Handle(CreateTenantCommand command)
    {
        // Validar que el plan de suscripción exista
        if (!await _subscriptionPlanAcl.ExistsByIdAsync(command.SubscriptionPlanId))
            throw new InvalidOperationException("El plan de suscripción especificado no existe.");

        // Crear el Tenant con todos los parámetros, incluyendo el SubscriptionPlanId
        var tenant = new Tenant(
            command.Ruc,
            command.LegalName,
            command.CommercialName,
            command.Address,
            command.City,
            command.Country,
            string.IsNullOrWhiteSpace(command.PhoneNumber) ? null : new PhoneNumber(command.PhoneNumber),
            string.IsNullOrWhiteSpace(command.Email) ? null : new EmailAddress(command.Email),
            command.Website,
            command.SubscriptionPlanId);

        // Guardar el Tenant en el repositorio
        await _repository.AddAsync(tenant);

        // Completar la transacción
        await _unitOfWork.CompleteAsync();

        // Retornar el ID del Tenant recién creado
        return (int)tenant.Id;
    }
} 