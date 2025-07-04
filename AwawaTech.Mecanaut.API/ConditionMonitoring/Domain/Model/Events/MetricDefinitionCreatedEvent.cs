using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Events;

public record MetricDefinitionCreatedEvent(long MetricDefinitionId, string Name, string Unit) : IDomainEvent; 