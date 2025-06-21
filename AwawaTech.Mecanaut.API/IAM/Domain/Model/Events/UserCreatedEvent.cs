using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Model.Events;

public record UserCreatedEvent(int UserId, string Username, long TenantId) : IDomainEvent; 