namespace AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;

/// <summary>
/// Entidad base con auditoría de fechas de creación y actualización.
/// </summary>
public abstract class AuditableEntity
{
    public long Id { get; protected set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void SetCreated(DateTime when) => CreatedAt = when;
    public void SetUpdated(DateTime when) => UpdatedAt = when;
} 