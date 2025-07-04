using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Subscription.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;


namespace AwawaTech.Mecanaut.API.IAM.Interfaces.ACL.Services;

public class SubscriptionPlanAcl : ISubscriptionPlanAcl
{
    private readonly ISubscriptionPlanRepository _repository;
    private readonly ITenantRepository _tenantRepository;

    public SubscriptionPlanAcl(ISubscriptionPlanRepository repository,  ITenantRepository tenantRepository)
    {
        _repository = repository;
        _tenantRepository = tenantRepository;
    }

    public async Task<bool> ExistsByIdAsync(long subscriptionPlanId)
    {
        var plan = await _repository.GetByIdAsync(subscriptionPlanId);
        return plan != null;
    }
    
    public async Task<int> GetMaxUsersByTenantId(long tenantId)
    {
        // Obtiene el SubscriptionPlanId asociado con el tenant
        var subscriptionPlanId = await _tenantRepository.GetSubscriptionPlanIdByTenantId(tenantId);
    
        // Obtiene el plan de suscripción correspondiente
        var subscriptionPlan = await _repository.GetByIdAsync(subscriptionPlanId);
    
        // Devuelve la cantidad máxima de usuarios permitidos en ese plan
        return subscriptionPlan.Features.MaxUsers;
    }
} 