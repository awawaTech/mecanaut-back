using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record CreateProductionLineResource(
    [Required] string Name,
    [Required] string Code,
    [Required] double CapacityUnitsPerHour,
    [Required] long PlantId); 