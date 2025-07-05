using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Events;

public record MachineAddedToPlanEvent(long PlanId, long MachineId, long TenantId) : IDomainEvent; 