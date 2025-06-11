using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.CommandServices;

public class PlantCommandService(
    IPlantRepository plants,
    IUnitOfWork unitOfWork,
    ITenantProvider tenantProvider) : IPlantCommandService
{
    public async Task<Plant> Handle(CreatePlantCommand command, CancellationToken ct = default)
    {
        // Unique name per tenant check
        var tenant = tenantProvider.Current;
        if (await plants.ExistsNameAsync(tenant, command.Name, ct))
            throw new Exception($"Plant name '{command.Name}' already exists for tenant.");

        var plant = Plant.Create(tenant, command.Name, command.Location);
        await plants.AddAsync(plant);
        await unitOfWork.CompleteAsync();
        return plant;
    }

    public async Task<ProductionLine?> Handle(AddProductionLineCommand command, CancellationToken ct = default)
    {
        var plant = await plants.FindByIdAsync(command.PlantId, ct);
        if (plant is null) return null;
        if (plant.Tenant != tenantProvider.Current) return null; // security guard

        var line = plant.AddLine(command.Name, command.Capacity);
        plants.Update(plant);
        await unitOfWork.CompleteAsync();
        return line;
    }
}
