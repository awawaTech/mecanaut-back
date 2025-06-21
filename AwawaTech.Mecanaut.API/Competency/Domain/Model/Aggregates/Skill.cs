using AwawaTech.Mecanaut.API.Competency.Domain.Model.Events;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;

public class Skill : AuditableAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public SkillStatus Status { get; private set; }
    public TenantId TenantId { get; private set; }

    protected Skill() { }

    private Skill(string name, string? description, string? category, TenantId tenantId)
    {
        Name        = name;
        Description = description;
        Category    = category;
        Status      = SkillStatus.Active;
        TenantId    = tenantId;
        AddDomainEvent(new SkillCreatedEvent(Id, tenantId.Value, name));
    }

    public static Skill Create(string name, string? description, string? category, TenantId tenantId)
        => new(name, description, category, tenantId);

    public void Update(string name, string? description, string? category)
    {
        Name        = name;
        Description = description;
        Category    = category;
        AddDomainEvent(new SkillUpdatedEvent(Id, TenantId.Value));
    }

    public void Deactivate()
    {
        Status = SkillStatus.Inactive;
        AddDomainEvent(new SkillDeactivatedEvent(Id, TenantId.Value));
    }

    public bool IsActive() => Status == SkillStatus.Active;
} 