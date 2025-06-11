using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.ACL;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.ACL;


/// <summary>
///   Facade used by other bounded contexts to interact with AssetManagement
///   without coupling to its internal model.
/// </summary>
public class AssetManagementContextFacade(
    IPlantCommandService plantCmd,
    IPlantQueryService plantQry,
    IProductionLineCommandService lineCmd,
    IProductionLineQueryService lineQry,
    IMachineryCommandService machCmd,
    IMachineryQueryService machQry)
        : IAssetManagementContextFacade
{
    public async Task<Guid> CreatePlantAsync(string name, string location, CancellationToken ct = default)
    {
        var cmd = new CreatePlantCommand(name, location);
        var plant = await plantCmd.Handle(cmd, ct);
        return plant.Id.Value;
    }

    public async Task<Guid?> AddLineToPlantAsync(Guid plantId, string name, int capacity, CancellationToken ct = default)
    {
        var cmd = new AddProductionLineCommand(new PlantId(plantId), name, capacity);
        var line = await lineCmd.Handle(cmd, ct);
        return line?.Id.Value;
    }

    public async Task<Guid?> AddMachineryAsync(Guid lineId, string model, string brand, CancellationToken ct = default)
    {
        var cmd = new CreateMachineryCommand(new ProductionLineId(lineId), model, brand);
        var mach = await machCmd.Handle(cmd, ct);
        return mach?.Id.Value;
    }

    // ─────────────────── QUERIES ───────────────────
    public Task<IEnumerable<Plant>> GetAllPlantsAsync(CancellationToken ct = default) =>
        plantQry.Handle(new GetAllPlantsQuery(), ct);

    public Task<IEnumerable<ProductionLine>> GetLinesByPlantAsync(Guid plantId, CancellationToken ct = default) =>
        lineQry.Handle(new GetProductionLinesByPlantQuery(new PlantId(plantId)), ct);

    public Task<IEnumerable<Machinery>> GetMachineryByLineAsync(Guid lineId, CancellationToken ct = default) =>
        machQry.Handle(new GetMachineryByLineQuery(new ProductionLineId(lineId)), ct);
}