using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

public record CreateProductionLineCommand(string Name, string Code, Capacity Capacity, long PlantId); 