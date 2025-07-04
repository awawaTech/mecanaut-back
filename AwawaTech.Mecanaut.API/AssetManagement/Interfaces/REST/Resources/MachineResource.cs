namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record MachineResource(
    long Id,
    string SerialNumber,
    string Name,
    string Manufacturer,
    string Model,
    string Type,
    double PowerConsumption,
    string Status,
    long? ProductionLineId,
    DateTime? LastMaintenanceDate,
    DateTime? NextMaintenanceDate);