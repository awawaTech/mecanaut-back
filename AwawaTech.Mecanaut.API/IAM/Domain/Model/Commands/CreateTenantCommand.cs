namespace AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;

public record CreateTenantCommand
{
    public string Ruc { get; init; } = string.Empty;
    public string LegalName { get; init; } = string.Empty;
    public string? CommercialName { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public string? Website { get; init; }
    public long SubscriptionPlanId { get; init; }
} 