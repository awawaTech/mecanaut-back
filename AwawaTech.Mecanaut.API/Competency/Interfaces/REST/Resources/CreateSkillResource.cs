using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Resources;

public record CreateSkillResource(
    [Required] string Name,
    string? Description,
    string? Category); 