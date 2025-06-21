namespace AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;

public record CreateTenantCommand(
    string Ruc,
    string LegalName,
    string? CommercialName,
    string? Address,
    string? City,
    string? Country,
    string? Phone,
    string? Email,
    string? Website); 