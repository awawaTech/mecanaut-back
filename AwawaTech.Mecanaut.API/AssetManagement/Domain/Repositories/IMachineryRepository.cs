using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;

public interface IMachineryRepository : IBaseRepository<Machinery>
{
    Task<IEnumerable<Machinery>> FindByLineIdAsync(ProductionLineId lineId, CancellationToken ct = default);
}