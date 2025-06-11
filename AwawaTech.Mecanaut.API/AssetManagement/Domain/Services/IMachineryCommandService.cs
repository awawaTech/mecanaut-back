using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

/// <summary>
/// Orquesta los comandos que crean o modifican maquinarias
/// </summary>
public interface IMachineryCommandService
{
    /// <summary>
    /// Crea una nueva maquinaria dentro de la línea indicada
    /// </summary>
    Task<Machinery?> Handle(CreateMachineryCommand command,
                            CancellationToken ct = default);
}
