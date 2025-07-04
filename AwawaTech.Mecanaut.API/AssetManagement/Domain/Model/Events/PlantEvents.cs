using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Events;

public record PlantCreatedEvent(long PlantId, string Name, long TenantId) : IDomainEvent;
public record PlantActivatedEvent(long PlantId, string Name) : IDomainEvent;
public record PlantDeactivatedEvent(long PlantId, string Name) : IDomainEvent; 