using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Events;

public record WorkOrderCreationRequestedEvent(
    string WorkOrderType,
    string Title,
    string Description,
    long MachineId,
    IReadOnlyCollection<long> RequiredSkillIds,
    long TenantId,
    long OriginPlanId,
    long OriginTaskId) : IDomainEvent; 