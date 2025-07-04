using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Transform;

public static class SubscriptionPlanResourceFromEntityAssembler
{
    public static SubscriptionPlanResource ToResourceFromEntity(SubscriptionPlan entity)
    {
        return new SubscriptionPlanResource
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Currency = entity.Currency,
            MaxMachines = entity.Features.MaxMachines,
            MaxUsers = entity.Features.MaxUsers,
            SupportPriority = entity.Features.SupportPriority,
            PredictiveMaintenance = entity.Features.PredictiveMaintenance,
            AdvancedAnalytics = entity.Features.AdvancedAnalytics,
            Status = entity.Status.Value,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
