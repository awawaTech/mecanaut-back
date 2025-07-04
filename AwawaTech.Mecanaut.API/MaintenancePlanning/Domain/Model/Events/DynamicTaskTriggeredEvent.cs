using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Events;

public record DynamicTaskTriggeredEvent(long PlanId, long TaskId, long MachineId, double Reading) : IDomainEvent; 