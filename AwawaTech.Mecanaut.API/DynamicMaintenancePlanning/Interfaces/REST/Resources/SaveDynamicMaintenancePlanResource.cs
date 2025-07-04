using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources;

public class SaveDynamicMaintenancePlanResource
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public string MetricId { get; set; }
} 