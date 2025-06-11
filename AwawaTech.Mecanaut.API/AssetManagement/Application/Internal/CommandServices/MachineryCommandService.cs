using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.CommandServices;

public class MachineryCommandService(
    IProductionLineRepository lines,
    IUnitOfWork unitOfWork,
    ITenantProvider tenantProvider)
    : IMachineryCommandService
{
    public async Task<Machinery?> Handle(CreateMachineryCommand command,
                                         CancellationToken ct = default)
    {
        // 1. Obtener línea y validar tenant
        var line = (await lines.FindByPlantIdAsync(PlantId.New(), ct))   // ← puedes exponer un FindByIdAsync en tu repo
                     .FirstOrDefault(l => l.Id == command.LineId);
        if (line is null || line.Tenant != tenantProvider.Current) return null;

        // 2. Crear maquinaria usando método de dominio
        var machine = line.AddMachinery(command.Model, command.Brand);

        // 3. Persistir cambios
        lines.Update(line);
        await unitOfWork.CompleteAsync();

        return machine;
    }
}
