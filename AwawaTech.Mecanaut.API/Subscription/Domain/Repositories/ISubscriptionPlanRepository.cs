using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Aggregates;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Repositories
{
    public interface ISubscriptionPlanRepository : IBaseRepository<SubscriptionPlan>
    {
        Task<SubscriptionPlan> GetByIdAsync(long id);
        Task<IEnumerable<SubscriptionPlan>> GetAllAsync();
        Task AddAsync(SubscriptionPlan plan);
        Task UpdateAsync(SubscriptionPlan plan);
        Task DeleteAsync(long id);
        
        Task<bool> ExistsByNameAsync(string name);
    }
}
