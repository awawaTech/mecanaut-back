using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.CommandServices;

public class PlantCommandService : IPlantCommandService
{
    private readonly IPlantRepository plantRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly TenantContextHelper tenantHelper;

    public PlantCommandService(IPlantRepository repo, IUnitOfWork uow, TenantContextHelper helper)
    {
        plantRepository = repo;
        unitOfWork      = uow;
        tenantHelper    = helper;
    }

    public async Task<Plant> Handle(CreatePlantCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        if (await plantRepository.ExistsByNameAsync(command.Name, tenantId))
            throw new InvalidOperationException("Plant name already exists");

        var plant = Plant.Create(command.Name, command.Location, command.ContactInfo, new TenantId(tenantId));
        await plantRepository.AddAsync(plant);
        await unitOfWork.CompleteAsync();
        return plant;
    }

    public async Task<Plant> Handle(UpdatePlantCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var plant = await plantRepository.FindByIdAndTenantAsync(command.PlantId, tenantId)
                     ?? throw new KeyNotFoundException("Plant not found");
        if (!plant.Name.Equals(command.Name, StringComparison.OrdinalIgnoreCase) &&
            await plantRepository.ExistsByNameAsync(command.Name, tenantId))
            throw new InvalidOperationException("Plant name already exists");

        plant.UpdateInfo(command.Name, command.Location, command.ContactInfo);
        await unitOfWork.CompleteAsync();
        return plant;
    }

    public async Task<Plant> Handle(ActivatePlantCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var plant = await plantRepository.FindByIdAndTenantAsync(command.PlantId, tenantId)
                     ?? throw new KeyNotFoundException("Plant not found");
        plant.Activate();
        await unitOfWork.CompleteAsync();
        return plant;
    }

    public async Task<Plant> Handle(DeactivatePlantCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var plant = await plantRepository.FindByIdAndTenantAsync(command.PlantId, tenantId)
                     ?? throw new KeyNotFoundException("Plant not found");
        var activeLines = await plantRepository.CountActiveProductionLinesAsync(command.PlantId, tenantId);
        if (activeLines > 0)
            throw new InvalidOperationException("Cannot deactivate plant with active production lines");
        plant.Deactivate();
        await unitOfWork.CompleteAsync();
        return plant;
    }
} 