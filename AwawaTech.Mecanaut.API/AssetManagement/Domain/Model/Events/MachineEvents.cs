using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Events;

public record MachineAssignedEvent(long MachineId, long ProductionLineId) : IDomainEvent;
public record MachineMaintenanceStartedEvent(long MachineId, MachineStatus PreviousStatus) : IDomainEvent;
public record MachineMaintenanceCompletedEvent(long MachineId) : IDomainEvent; 