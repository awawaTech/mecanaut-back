using System;
using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.Events
{
    public record SubscriptionPlanCreatedEvent : IDomainEvent
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public SubscriptionFeatures Features { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }

    public record SubscriptionPlanUpdatedEvent : IDomainEvent
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public SubscriptionFeatures Features { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }

    public record SubscriptionPlanStatusChangedEvent : IDomainEvent
    {
        public long Id { get; private set; }
        public SubscriptionStatus OldStatus { get; private set; }
        public SubscriptionStatus NewStatus { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
