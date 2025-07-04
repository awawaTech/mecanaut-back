using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Events;

public record MaintenancePlanCreatedEvent(long PlanId, long TenantId, PlanType PlanType) : IDomainEvent; 