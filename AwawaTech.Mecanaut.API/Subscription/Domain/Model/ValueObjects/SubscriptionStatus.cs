using System;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects
{
    public record SubscriptionStatus
    {
        public string Value { get; private set; }

        private SubscriptionStatus(string value)
        {
            Value = value;
        }

        public static SubscriptionStatus Active => new("ACTIVE");
        public static SubscriptionStatus Inactive => new("INACTIVE");
        public static SubscriptionStatus Suspended => new("SUSPENDED");
        public static SubscriptionStatus Trial => new("TRIAL");

        public static SubscriptionStatus FromString(string status)
        {
            return status.ToUpper() switch
            {
                "ACTIVE" => Active,
                "INACTIVE" => Inactive,
                "SUSPENDED" => Suspended,
                "TRIAL" => Trial,
                _ => throw new ArgumentException($"Estado de suscripción inválido: {status}")
            };
        }

        public bool IsActive() => this == Active || this == Trial;
    }
} 