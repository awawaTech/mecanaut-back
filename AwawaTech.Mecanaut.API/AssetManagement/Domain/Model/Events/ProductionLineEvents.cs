using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Events;

public record ProductionLineCreatedEvent(long ProductionLineId, string Name, long PlantId, long TenantId) : IDomainEvent;
public record ProductionStartedEvent(long ProductionLineId, string Name, long PlantId, ProductionLineStatus PreviousStatus) : IDomainEvent;
public record ProductionStoppedEvent(long ProductionLineId, string Name, long PlantId, string Reason) : IDomainEvent; 