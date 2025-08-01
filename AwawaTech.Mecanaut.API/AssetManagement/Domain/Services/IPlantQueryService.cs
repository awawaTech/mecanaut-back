using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IPlantQueryService
{
    Task<IEnumerable<Plant>> Handle(GetAllPlantsQuery query);
    Task<Plant?> Handle(GetPlantByIdQuery query);
} 