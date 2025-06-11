using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

public record CreateMachineryCommand(ProductionLineId LineId, string Model, string Brand);