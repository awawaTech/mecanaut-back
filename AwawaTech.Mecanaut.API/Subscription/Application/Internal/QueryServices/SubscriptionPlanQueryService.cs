using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.Subscription.Domain.Repositories;
using AwawaTech.Mecanaut.API.Subscription.Domain.Services;

namespace AwawaTech.Mecanaut.API.Subscription.Application.Internal.QueryServices
{
    public class SubscriptionPlanQueryService : ISubscriptionPlanQueryService
    {
        private readonly ISubscriptionPlanRepository _repository;

        public SubscriptionPlanQueryService(ISubscriptionPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<SubscriptionPlan> HandleAsync(GetSubscriptionPlanByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id);
        }

        public async Task<IEnumerable<SubscriptionPlan>> HandleAsync(GetAllSubscriptionPlansQuery query)
        {
            return await _repository.GetAllAsync();
        }
    }
} 