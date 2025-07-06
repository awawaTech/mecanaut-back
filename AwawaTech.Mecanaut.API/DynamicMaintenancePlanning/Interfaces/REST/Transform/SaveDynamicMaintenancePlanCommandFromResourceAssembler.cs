using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Transform
{
    public class SaveDynamicMaintenancePlanCommandFromResourceAssembler
    {
        public CreateDynamicMaintenancePlanCommand ToCommand(SaveDynamicMaintenancePlanResource resource)
        {
            // Aquí se realiza la conversión
            return new CreateDynamicMaintenancePlanCommand
            {
                Name = resource.Name,
                MetricId = resource.MetricId, // Se asigna como string
                Amount = double.Parse(resource.Amount),
                ProductionLineId = resource.ProductionLineId,  // Se asigna como string
                PlantLineId = resource.PlantLineId,  // Se asigna como string
                Machines = resource.Machines ?? new List<long>(),  // Se asegura de que no sea null
                Tasks = resource.Tasks ?? new List<string>()  // Se asegura de que no sea null
            };
        }
    }
}