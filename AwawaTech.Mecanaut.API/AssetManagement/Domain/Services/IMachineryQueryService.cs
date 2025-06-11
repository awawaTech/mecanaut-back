using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

public interface IMachineryQueryService
{
    Task<IEnumerable<Machinery>> Handle(GetMachineryByLineQuery query, CancellationToken ct = default);
}