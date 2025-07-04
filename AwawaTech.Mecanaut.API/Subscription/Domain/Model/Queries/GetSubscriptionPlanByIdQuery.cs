using System;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.Queries
{
    public record GetSubscriptionPlanByIdQuery
    {
        public long Id { get;  set; }    
    }
}
