using System;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects
{
    public record SubscriptionFeatures
    {
        public int MaxMachines { get; private set; }
        public int MaxUsers { get; private set; }
        public bool SupportPriority { get; private set; }
        public bool PredictiveMaintenance { get; private set; }
        public bool AdvancedAnalytics { get; private set; }

        private SubscriptionFeatures() { }

        public static SubscriptionFeatures Create(
            int maxMachines,
            int maxUsers,
            bool supportPriority = false,
            bool predictiveMaintenance = false,
            bool advancedAnalytics = false)
        {
            if (maxMachines < 0) throw new ArgumentException("El número máximo de máquinas no puede ser negativo");
            if (maxUsers < 0) throw new ArgumentException("El número máximo de usuarios no puede ser negativo");

            return new SubscriptionFeatures
            {
                MaxMachines = maxMachines,
                MaxUsers = maxUsers,
                SupportPriority = supportPriority,
                PredictiveMaintenance = predictiveMaintenance,
                AdvancedAnalytics = advancedAnalytics
            };
        }
    }
} 