namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    // Tenant data
    string Ruc,
    string LegalName,
    string? CommercialName,
    string? Address,
    string? City,
    string? Country,
    string? TenantPhone,
    string TenantEmail,
    string? Website,
    long SubscriptionPlanId,

    // Admin user data
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName
);