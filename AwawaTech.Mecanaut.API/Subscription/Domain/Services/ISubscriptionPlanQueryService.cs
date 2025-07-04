using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Services
{
    public interface ISubscriptionPlanQueryService
    {
        Task<SubscriptionPlan> HandleAsync(GetSubscriptionPlanByIdQuery query);
        Task<IEnumerable<SubscriptionPlan>> HandleAsync(GetAllSubscriptionPlansQuery query);
    }
}
