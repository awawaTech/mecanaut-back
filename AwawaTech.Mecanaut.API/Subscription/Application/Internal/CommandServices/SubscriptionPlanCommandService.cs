using System;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Subscription.Domain.Repositories;
using AwawaTech.Mecanaut.API.Subscription.Domain.Services;

namespace AwawaTech.Mecanaut.API.Subscription.Application.Internal.CommandServices
{
    public class SubscriptionPlanCommandService : ISubscriptionPlanCommandService
    {
        private readonly ISubscriptionPlanRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _eventDispatcher;
        
        private static readonly SubscriptionPlan[] DefaultPlans =
        {
            SubscriptionPlan.Create("Plan Gratuito", "Plan de suscripción gratuito con funcionalidades básicas.", 0.00m, "S/.", SubscriptionFeatures.Create(1, 1)),
            SubscriptionPlan.Create("Plan Profesional", "Plan profesional con características adicionales.", 15.00m, "S/.", SubscriptionFeatures.Create(3, 5)),
            SubscriptionPlan.Create("Plan Corporativo", "Plan corporativo con todas las características.", 30.00m, "S/.", SubscriptionFeatures.Create(3, 100))
        };
        
        public SubscriptionPlanCommandService(
            ISubscriptionPlanRepository repository,
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher eventDispatcher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(SeedSubscriptionPlansCommand command)
        {
            foreach (var plan in DefaultPlans)
            {
                if (!await _repository.ExistsByNameAsync(plan.Name))
                {
                    await _repository.AddAsync(plan);
                }
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<long> HandleAsync(CreateSubscriptionPlanCommand command)
        {
            var features = SubscriptionFeatures.Create(
                command.MaxMachines,
                command.MaxUsers,
                command.SupportPriority,
                command.PredictiveMaintenance,
                command.AdvancedAnalytics);

            var plan = SubscriptionPlan.Create(
                command.Name,
                command.Description,
                command.Price,
                command.Currency,
                features);

            await _repository.AddAsync(plan);
            await _unitOfWork.CompleteAsync();
            return plan.Id;
        }

        public async Task HandleAsync(UpdateSubscriptionPlanCommand command)
        {
            var plan = await _repository.GetByIdAsync(command.Id);
            if (plan == null)
                throw new InvalidOperationException($"Plan de suscripción no encontrado con ID: {command.Id}");

            var features = SubscriptionFeatures.Create(
                command.MaxMachines,
                command.MaxUsers,
                command.SupportPriority,
                command.PredictiveMaintenance,
                command.AdvancedAnalytics);

            plan.Update(
                command.Name,
                command.Description,
                command.Price,
                command.Currency,
                features);

            await _repository.UpdateAsync(plan);
            await _unitOfWork.CompleteAsync();
        }

        public async Task HandleAsync(ChangeSubscriptionPlanStatusCommand command)
        {
            var plan = await _repository.GetByIdAsync(command.Id);
            if (plan == null)
                throw new InvalidOperationException($"Plan de suscripción no encontrado con ID: {command.Id}");

            // Accede a la propiedad 'Value' de SubscriptionStatus
            var status = command.Status.Value.ToUpper();  // Aquí accedemos al valor de tipo string

            switch (status)
            {
                case "ACTIVE":
                    plan.Activate();
                    break;
                case "INACTIVE":
                    plan.Deactivate();
                    break;
                case "SUSPENDED":
                    plan.Suspend();
                    break;
                default:
                    throw new InvalidOperationException($"Estado de suscripción inválido: {command.Status.Value}");
            }

            await _repository.UpdateAsync(plan);
            await _unitOfWork.CompleteAsync();
        }

    }
} 