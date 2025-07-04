using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Services;

public interface ISubscriptionPlanCommandService
{
    Task<long> HandleAsync(CreateSubscriptionPlanCommand command);
    Task HandleAsync(UpdateSubscriptionPlanCommand command);
    Task HandleAsync(ChangeSubscriptionPlanStatusCommand command);
    Task HandleAsync(SeedSubscriptionPlansCommand command);
}
