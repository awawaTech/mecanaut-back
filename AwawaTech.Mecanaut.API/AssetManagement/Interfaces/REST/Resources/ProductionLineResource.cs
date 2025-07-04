namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record ProductionLineResource(
    long Id,
    string Name,
    string Code,
    double CapacityUnitsPerHour,
    string Status,
    long PlantId); 