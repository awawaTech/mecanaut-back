using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;

namespace AssetManagement.Application.Internal.QueryServices;

public class MachineryQueryService(IMachineryRepository machinery)
    : IMachineryQueryService
{
    public async Task<IEnumerable<Machinery>> Handle(GetMachineryByLineQuery query, CancellationToken ct = default)
        => await machinery.FindByLineIdAsync(query.LineId, ct);
}
