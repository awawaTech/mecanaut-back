using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IProductionLineCommandService
{
    Task<ProductionLine?> Handle(AddProductionLineCommand command, CancellationToken ct = default);
    Task<Machinery?> Handle(CreateMachineryCommand command, CancellationToken ct = default);
}