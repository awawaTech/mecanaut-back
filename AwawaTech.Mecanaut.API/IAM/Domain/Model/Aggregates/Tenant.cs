using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;

/// <summary>
/// Raíz de agregado Tenant que representa una organización o cliente multi-tenant.
/// </summary>
public class Tenant : AuditableAggregateRoot
{
    public string Ruc { get; private set; } = string.Empty;
    public string LegalName { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? CommercialName { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? Country { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public EmailAddress? Email { get; private set; }
    public string? Website { get; private set; }
    public bool Active { get; private set; }

    private Tenant() { }

    public Tenant(string ruc, string legalName, string? commercialName,
        string? address, string? city, string? country,
        PhoneNumber? phone, EmailAddress? email, string? website)
    {
        Active = true;
        Ruc = ruc;
        LegalName = legalName;
        Code = GenerateCodeFromRuc(ruc);
        CommercialName = commercialName;
        Address = address;
        City = city;
        Country = country;
        PhoneNumber = phone;
        Email = email;
        Website = website;
    }

    private static string GenerateCodeFromRuc(string ruc)
    {
        if (!string.IsNullOrEmpty(ruc) && ruc.Length >= 6)
            return $"TENANT_{ruc[^6..]}";
        return $"TENANT_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 1000000}";
    }

    public void Activate() => Active = true;
    public void Deactivate() => Active = false;
    public bool IsActive() => Active;

    public string GetDisplayName() => !string.IsNullOrWhiteSpace(CommercialName) ? CommercialName! : LegalName;
} 