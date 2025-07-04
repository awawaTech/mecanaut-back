using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities
{
    public class DynamicMaintenancePlanWithDetails
    {
        public DynamicMaintenancePlan Plan { get; set; } // Plan base
        public List<DynamicMaintenancePlanMachine> Machines { get; set; } // Máquinas asociadas
        public List<DynamicMaintenancePlanTask> Tasks { get; set; } // Tareas asociadas
    }
}