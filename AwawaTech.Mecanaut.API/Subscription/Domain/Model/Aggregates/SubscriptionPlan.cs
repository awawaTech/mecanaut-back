using System;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.Aggregates
{
    public class SubscriptionPlan : AuditableAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public SubscriptionFeatures Features { get; private set; }
        public SubscriptionStatus Status { get; private set; }

        private SubscriptionPlan() { }

        public static SubscriptionPlan Create(
            string name,
            string description,
            decimal price,
            string currency,
            SubscriptionFeatures features)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del plan no puede estar vacío");
            
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("La descripción del plan no puede estar vacía");
            
            if (price < 0)
                throw new ArgumentException("El precio no puede ser negativo");
            
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("La moneda no puede estar vacía");

            return new SubscriptionPlan
            {
                Name = name,
                Description = description,
                Price = price,
                Currency = currency,
                Features = features,
                Status = SubscriptionStatus.Active
            };
        }

        public void Update(
            string name,
            string description,
            decimal price,
            string currency,
            SubscriptionFeatures features)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del plan no puede estar vacío");
            
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("La descripción del plan no puede estar vacía");
            
            if (price < 0)
                throw new ArgumentException("El precio no puede ser negativo");
            
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("La moneda no puede estar vacía");

            Name = name;
            Description = description;
            Price = price;
            Currency = currency;
            Features = features;
        }

        public void Activate()
        {
            if (Status == SubscriptionStatus.Active)
                throw new InvalidOperationException("El plan ya está activo");

            Status = SubscriptionStatus.Active;
        }

        public void Deactivate()
        {
            if (Status == SubscriptionStatus.Inactive)
                throw new InvalidOperationException("El plan ya está inactivo");

            Status = SubscriptionStatus.Inactive;
        }

        public void Suspend()
        {
            if (Status == SubscriptionStatus.Suspended)
                throw new InvalidOperationException("El plan ya está suspendido");

            Status = SubscriptionStatus.Suspended;
        }

        public bool CanAddMachine(int currentMachines)
        {
            return Status.IsActive() && currentMachines < Features.MaxMachines;
        }

        public bool CanAddUser(int currentUsers)
        {
            return Status.IsActive() && currentUsers < Features.MaxUsers;
        }
    }
}
