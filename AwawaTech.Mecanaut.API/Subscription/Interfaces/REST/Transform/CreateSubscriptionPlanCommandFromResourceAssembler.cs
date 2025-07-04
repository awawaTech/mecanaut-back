using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Transform;

public static class CreateSubscriptionPlanCommandFromResourceAssembler
{
    public static CreateSubscriptionPlanCommand ToCommandFromResource(CreateSubscriptionPlanResource resource)
        => new()
        {
            Name = resource.Name,
            Description = resource.Description,
            Price = resource.Price,
            Currency = resource.Currency,
            MaxMachines = resource.MaxMachines,
            MaxUsers = resource.MaxUsers,
            SupportPriority = resource.SupportPriority,
            PredictiveMaintenance = resource.PredictiveMaintenance,
            AdvancedAnalytics = resource.AdvancedAnalytics
        };
}
