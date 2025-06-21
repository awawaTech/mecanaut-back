using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IProductionLineCommandService
{
    Task<ProductionLine> Handle(CreateProductionLineCommand command);
    Task<ProductionLine> Handle(StartProductionCommand command);
    Task<ProductionLine> Handle(StopProductionCommand command);
} 