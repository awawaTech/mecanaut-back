using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IPlantCommandService
{
    Task<Plant> Handle(CreatePlantCommand command);
    Task<Plant> Handle(UpdatePlantCommand command);
    Task<Plant> Handle(ActivatePlantCommand command);
    Task<Plant> Handle(DeactivatePlantCommand command);
} 