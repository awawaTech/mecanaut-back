namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

public record StopProductionCommand(long ProductionLineId, string Reason); 