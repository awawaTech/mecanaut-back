using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IProductionLineQueryService
{
    Task<IEnumerable<ProductionLine>> Handle(GetProductionLinesByPlantQuery query);
    Task<ProductionLine?> Handle(GetProductionLineByIdQuery query);
    Task<IEnumerable<ProductionLine>> Handle(GetRunningProductionLinesQuery query);
} 