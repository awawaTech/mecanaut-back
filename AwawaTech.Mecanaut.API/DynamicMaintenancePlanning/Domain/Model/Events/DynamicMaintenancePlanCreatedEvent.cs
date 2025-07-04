using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Events;

public record DynamicMaintenancePlanCreatedEvent(long PlanId, long TenantId, string Name) : IDomainEvent; 