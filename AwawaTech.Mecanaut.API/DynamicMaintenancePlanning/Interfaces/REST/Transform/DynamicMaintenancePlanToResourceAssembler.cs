using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Transform;

public class DynamicMaintenancePlanToResourceAssembler
{
    public DynamicMaintenancePlanResource ToResource(DynamicMaintenancePlan plan)
    {
        return new DynamicMaintenancePlanResource
        {
            Id = plan.Id.ToString(),
            Name = plan.Name,
            MetricId = plan.MetricId.ToString(),
            Amount = plan.Amount,
        };
    }
} 