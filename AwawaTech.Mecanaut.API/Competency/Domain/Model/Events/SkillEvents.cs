using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.Competency.Domain.Model.Events;

public record SkillCreatedEvent(long SkillId, long TenantId, string Name) : IDomainEvent;
public record SkillUpdatedEvent(long SkillId, long TenantId) : IDomainEvent;
public record SkillDeactivatedEvent(long SkillId, long TenantId) : IDomainEvent; 