using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.IAM.Interfaces.ACL;

public interface ISubscriptionPlanAcl
{
    Task<bool> ExistsByIdAsync(long subscriptionPlanId);
    Task<int> GetMaxUsersByTenantId(long tenantId);
} 