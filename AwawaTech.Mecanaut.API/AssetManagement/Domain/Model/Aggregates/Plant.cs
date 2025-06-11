using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Enums;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;

public class Plant : ITenantScopedAggregate
{
    private readonly List<ProductionLine> _lines = [];
    public PlantId Id { get; }
    public TenantId Tenant { get; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public PlantStatus Status { get; private set; }

    public IReadOnlyList<ProductionLine> Lines => _lines.AsReadOnly();

    private Plant(PlantId id, TenantId tenant, string name, string location)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(location)) throw new ArgumentException("Location cannot be empty", nameof(location));

        Id = id;
        Tenant = tenant;
        Name = name;
        Location = location;
        Status = PlantStatus.Draft;
    }



    public static Plant Create(TenantId tenant, string name, string location) =>
        new(PlantId.New(), tenant, name, location);

    public ProductionLine AddLine(string name, int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be > 0");
        var line = ProductionLine.Create(Tenant, Id, name, capacity);
        _lines.Add(line);
        return line;
    }

    public void Activate() => Status = PlantStatus.Active;
    public void Deactivate() => Status = PlantStatus.Deactivated;
}