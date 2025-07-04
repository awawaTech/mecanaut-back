using AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Events;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;

public class ProductionLine : AuditableAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public Capacity Capacity { get; private set; }
    public ProductionLineStatus Status { get; private set; }
    public long PlantId { get; private set; }
    public TenantId TenantId { get; private set; }

    private ProductionLine(string name, string code, Capacity capacity, long plantId, TenantId tenantId)
    {
        ValidateName(name);
        ValidateCode(code);

        Name     = name;
        Code     = code;
        Capacity = capacity;
        PlantId  = plantId;
        TenantId = tenantId;
        Status   = ProductionLineStatus.Ready;

        AddDomainEvent(new ProductionLineCreatedEvent(Id, name, plantId, tenantId.Value));
    }

    protected ProductionLine() { }

    public static ProductionLine Create(string name, string code, Capacity capacity, long plantId, TenantId tenantId)
        => new(name, code, capacity, plantId, tenantId);

    /* ---- Behaviour ---- */
    public void StartProduction()
    {
        ValidateCanStart();
        var previous = Status;
        Status = ProductionLineStatus.Running;
        AddDomainEvent(new ProductionStartedEvent(Id, Name, PlantId, previous));
    }

    public void StopProduction(string reason)
    {
        if (Status == ProductionLineStatus.Stopped)
            throw new ProductionLineAlreadyStoppedException();
        var prev = Status;
        Status = ProductionLineStatus.Stopped;
        AddDomainEvent(new ProductionStoppedEvent(Id, Name, PlantId, reason));
    }

    public void PutInMaintenance()
    {
        if (Status == ProductionLineStatus.Running)
            throw new ProductionLineInMaintenanceException("Cannot put running line in maintenance");
        Status = ProductionLineStatus.Maintenance;
    }

    public void MarkAsReady() => Status = ProductionLineStatus.Ready;

    public bool IsRunning() => Status == ProductionLineStatus.Running;
    public bool IsReady() => Status == ProductionLineStatus.Ready;

    public bool CanAddMachine() => Status != ProductionLineStatus.Maintenance && Status != ProductionLineStatus.Running;

    public void UpdateCapacity(Capacity capacity) => Capacity = capacity;
    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    /* ---- Validation helpers ---- */
    private void ValidateCanStart()
    {
        if (Status == ProductionLineStatus.Running)
            throw new ProductionLineAlreadyRunningException();
        if (Status != ProductionLineStatus.Ready)
            throw new ProductionLineNotReadyException();
        if (!Capacity.IsValid())
            throw new InvalidOperationException("Capacity not set");
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
    }

    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code required");
    }
} 