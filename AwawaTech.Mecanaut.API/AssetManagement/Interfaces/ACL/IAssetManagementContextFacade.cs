using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.ACL;

/// <summary>
/// Facade expuesta a otros Bounded Contexts para operar
/// con AssetManagement sin acoplarse a su modelo interno.
/// </summary>
public interface IAssetManagementContextFacade
{
    // ──────────────── COMMANDS ────────────────
    Task<Guid>  CreatePlantAsync(string name,
                                 string location,
                                 CancellationToken ct = default);

    Task<Guid?> AddLineToPlantAsync(Guid plantId,
                                    string name,
                                    int capacity,
                                    CancellationToken ct = default);

    Task<Guid?> AddMachineryAsync(Guid lineId,
                                  string model,
                                  string brand,
                                  CancellationToken ct = default);

    // ──────────────── QUERIES ────────────────
    Task<IEnumerable<Plant>> GetAllPlantsAsync(CancellationToken ct = default);

    Task<IEnumerable<ProductionLine>> GetLinesByPlantAsync(Guid plantId,
                                                           CancellationToken ct = default);

    Task<IEnumerable<Machinery>> GetMachineryByLineAsync(Guid lineId,
                                                         CancellationToken ct = default);
}
