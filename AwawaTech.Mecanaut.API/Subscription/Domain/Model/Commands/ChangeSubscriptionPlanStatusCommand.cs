using System;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands
{
    public record ChangeSubscriptionPlanStatusCommand
    {
        public long Id;
        public SubscriptionStatus Status;
    }
} 