using System;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands
{
    public record UpdateSubscriptionPlanCommand
    {
        public long Id ;
        public string Name ;
        public string Description ;
        public decimal Price ;
        public string Currency ;
        public int MaxMachines ;
        public int MaxUsers ;
        public bool SupportPriority ;
        public bool PredictiveMaintenance ;
        public bool AdvancedAnalytics ;
    }
}
