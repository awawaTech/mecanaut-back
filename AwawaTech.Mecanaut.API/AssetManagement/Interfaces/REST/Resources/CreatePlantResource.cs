using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record CreatePlantResource(
    [Required] string Name,
    [Required] string Address,
    [Required] string City,
    [Required] string Country,
    [Required] string Phone,
    [Required, EmailAddress] string Email); 