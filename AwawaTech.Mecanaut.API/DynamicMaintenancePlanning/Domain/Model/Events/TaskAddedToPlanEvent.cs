using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Events;

public record TaskAddedToPlanEvent(long PlanId, string TaskDescription, long TenantId) : IDomainEvent; 