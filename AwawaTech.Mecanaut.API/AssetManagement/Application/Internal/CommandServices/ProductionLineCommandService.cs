using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.CommandServices;

public class ProductionLineCommandService(
    IProductionLineRepository lines,
    IPlantRepository plants,
    IUnitOfWork unitOfWork,
    ITenantProvider tenantProvider) : IProductionLineCommandService
{
    public async Task<ProductionLine?> Handle(AddProductionLineCommand command, CancellationToken ct = default)
    {
        // Delegated to PlantCommandService, keep here as placeholder
        throw new NotImplementedException();
    }

    public async Task<Machinery?> Handle(CreateMachineryCommand command, CancellationToken ct = default)
    {
        var line = (await lines.FindByPlantIdAsync(new PlantId(Guid.Empty), ct))
                        .FirstOrDefault(l => l.Id == command.LineId);
        if (line is null) return null;
        if (line.Tenant != tenantProvider.Current) return null;

        var mach = line.AddMachinery(command.Model, command.Brand);
        lines.Update(line);
        await unitOfWork.CompleteAsync();
        return mach;
    }
}
