using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record RegisterMachineResource(
    [Required] string SerialNumber,
    [Required] string Name,
    [Required] string Manufacturer,
    [Required] string Model,
    [Required] string Type,
    [Required] double PowerConsumption); 