

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources
{
    public class DynamicMaintenancePlanResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MetricId { get; set; }
        
        public long Amount { get; set; }

        // Lista de IDs de máquinas asociadas
        public List<long> MachineIds { get; set; } = new List<long>();

        // Lista de descripciones de tareas asociadas
        public List<string> TaskDescriptions { get; set; } = new List<string>();
    }
}
