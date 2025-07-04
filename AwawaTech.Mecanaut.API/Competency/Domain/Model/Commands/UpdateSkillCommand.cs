namespace AwawaTech.Mecanaut.API.Competency.Domain.Model.Commands;

public record UpdateSkillCommand(long SkillId, string Name, string? Description, string? Category); 