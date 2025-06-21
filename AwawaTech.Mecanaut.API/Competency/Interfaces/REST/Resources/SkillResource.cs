namespace AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Resources;

public record SkillResource(long Id,
                             string Name,
                             string? Description,
                             string? Category,
                             string Status); 