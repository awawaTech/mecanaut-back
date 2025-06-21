using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;

/// <summary>
/// Raíz de agregado con soporte a eventos de dominio.
/// </summary>
public abstract class AuditableAggregateRoot : AuditableEntity
{
    private readonly List<AwawaTech.Mecanaut.API.Shared.Domain.Events.IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<AwawaTech.Mecanaut.API.Shared.Domain.Events.IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(AwawaTech.Mecanaut.API.Shared.Domain.Events.IDomainEvent @event) => _domainEvents.Add(@event);
    public void ClearDomainEvents() => _domainEvents.Clear();
} 