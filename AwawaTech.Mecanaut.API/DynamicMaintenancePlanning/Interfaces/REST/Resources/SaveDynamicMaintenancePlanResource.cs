using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources
{
    public class SaveDynamicMaintenancePlanResource
    {
        public string Name { get; set; }
        public string MetricId { get; set; }  // Debe coincidir con el tipo string
        
        public string Amount { get; set; }
        public string ProductionLineId { get; set; }  // Cambiado a string
        public string PlantLineId { get; set; }  // Cambiado a string
        public List<long> Machines { get; set; } = new();  // Lista de máquinas (long)
        public List<string> Tasks { get; set; } = new();  // Lista de tareas (string)
    }
}
