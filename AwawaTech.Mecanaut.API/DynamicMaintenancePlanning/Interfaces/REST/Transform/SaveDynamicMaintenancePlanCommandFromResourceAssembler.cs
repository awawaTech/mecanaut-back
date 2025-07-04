using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Transform;

public class SaveDynamicMaintenancePlanCommandFromResourceAssembler
{
    public CreateDynamicMaintenancePlanCommand ToCommand(SaveDynamicMaintenancePlanResource resource)
    {
        return new CreateDynamicMaintenancePlanCommand
        {
            Name = resource.Name,
            MetricId = resource.MetricId
        };
    }
} 