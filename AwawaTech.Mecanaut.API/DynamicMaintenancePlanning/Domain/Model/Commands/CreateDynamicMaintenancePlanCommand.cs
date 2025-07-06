using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Commands
{
    public class CreateDynamicMaintenancePlanCommand
    {
        public string Name { get; set; }
        public string MetricId { get; set; }
        public double Amount { get;  set; }
        public string ProductionLineId { get; set; }
        
        public string PlantLineId { get; set; }
        public List<long> Machines { get; set; } = new();
        public List<string> Tasks { get; set; } = new();
    }

} 