using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Resources;

public record UpdateSkillResource(
    [Required] string Name,
    string? Description,
    string? Category); 