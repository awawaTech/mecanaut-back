using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Enums;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;

public class ProductionLine : ITenantScopedAggregate
{
    private readonly List<Machinery> _machinery = [];
    
    public ProductionLineId Id { get; }
    public TenantId Tenant { get; }
    public PlantId PlantId { get; }
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public LineStatus Status { get; private set; }

    private ProductionLine(ProductionLineId id, TenantId tenant, PlantId plantId, string name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));

        Id = id;
        Tenant = tenant;
        PlantId = plantId;
        Name = name;
        Capacity = capacity;
        Status = LineStatus.Active;
    }

    public IReadOnlyList<Machinery> Machinery => _machinery.AsReadOnly();

    public static ProductionLine Create(TenantId tenant, PlantId plantId, string name, int capacity) =>
        new(ProductionLineId.New(), tenant, plantId, name, capacity);

    public Machinery AddMachinery(string model, string brand)
    {
        var machine = Aggregates.Machinery.Create(Tenant, Id, model, brand);
        _machinery.Add(machine);
        return machine;
    }
}