using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IPlantCommandService
{
    Task<Plant> Handle(CreatePlantCommand command, CancellationToken ct = default);
    Task<ProductionLine?> Handle(AddProductionLineCommand command, CancellationToken ct = default);
}