
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Transform
{
    public class DynamicMaintenancePlanWithDetailsToResourceAssembler
    {
        public DynamicMaintenancePlanWithDetailsResource ToResource(DynamicMaintenancePlanWithDetails planWithDetails)
        {
            return new DynamicMaintenancePlanWithDetailsResource
            {
                Id = planWithDetails.Plan.Id.ToString(),
                Name = planWithDetails.Plan.Name,
                MetricId = planWithDetails.Plan.MetricId.ToString(),
                Amount = planWithDetails.Plan.Amount.ToString(),
                MachineIds = planWithDetails.Machines.Select(m => m.MachineId).ToList(),  // Extraemos los IDs de las máquinas
                TaskDescriptions = planWithDetails.Tasks.Select(t => t.TaskDescription).ToList() // Extraemos las descripciones de las tareas
            };
        }
    }
}
