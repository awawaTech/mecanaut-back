using AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Events;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;

public class Plant : AuditableAggregateRoot
{
    public string Name { get; private set; } = null!;
    public Location Location { get; private set; }
    public ContactInfo ContactInfo { get; private set; }
    public bool Active { get; private set; }
    public TenantId TenantId { get; private set; }

    private Plant(string name, Location location, ContactInfo contactInfo, TenantId tenantId)
    {
        ValidateName(name);
        Name        = name;
        Location    = location;
        ContactInfo = contactInfo;
        TenantId    = tenantId;
        Active      = true;
        AddDomainEvent(new PlantCreatedEvent(Id, name, tenantId.Value));
    }

    protected Plant() { }

    public static Plant Create(string name, Location location, ContactInfo contactInfo, TenantId tenantId)
        => new(name, location, contactInfo, tenantId);

    /* ------------ Behaviour ------------- */
    public void Activate()
    {
        if (Active) throw new PlantAlreadyActiveException($"Plant {Name} already active");
        Active = true;
        AddDomainEvent(new PlantActivatedEvent(Id, Name));
    }

    public void Deactivate()
    {
        if (!Active) throw new PlantAlreadyInactiveException($"Plant {Name} already inactive");
        Active = false;
        AddDomainEvent(new PlantDeactivatedEvent(Id, Name));
    }

    public bool IsActive() => Active;

    public bool CanAddProductionLine() => Active;

    public void UpdateContactInfo(ContactInfo info)
    {
        ContactInfo = info;
    }

    public void UpdateLocation(Location location)
    {
        Location = location;
    }

    public void UpdateInfo(string name, Location location, ContactInfo contact)
    {
        ValidateName(name);
        Name        = name;
        Location    = location;
        ContactInfo = contact;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (name.Length > 100) throw new ArgumentException("Name too long (>100)", nameof(name));
    }
} 