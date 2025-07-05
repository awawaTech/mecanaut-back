namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources
{
    public class DynamicMaintenancePlanWithDetailsResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MetricId { get; set; }
        
        public string Amount { get; set; }
        public List<long> MachineIds { get; set; } = new List<long>(); // IDs de máquinas asociadas
        public List<string> TaskDescriptions { get; set; } = new List<string>(); // Descripciones de tareas asociadas
    }
}