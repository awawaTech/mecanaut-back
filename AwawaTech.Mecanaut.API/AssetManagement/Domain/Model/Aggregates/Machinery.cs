using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Enums;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;

public class Machinery : ITenantScopedAggregate
{
    public MachineryId Id { get; }
    public TenantId Tenant { get; }
    public ProductionLineId LineId { get; }
    public string Model { get; private set; }
    public string Brand { get; private set; }
    public MachineryStatus Status { get; private set; }

    private Machinery(MachineryId id, TenantId tenant, ProductionLineId lineId, string model, string brand)
    {
        if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model cannot be empty", nameof(model));
        if (string.IsNullOrWhiteSpace(brand)) throw new ArgumentException("Brand cannot be empty", nameof(brand));

        Id = id;
        Tenant = tenant;
        LineId = lineId;
        Model = model;
        Brand = brand;
        Status = MachineryStatus.Operational;
    }


    public static Machinery Create(TenantId tenant, ProductionLineId lineId, string model, string brand) =>
        new(MachineryId.New(), tenant, lineId, model, brand);
}