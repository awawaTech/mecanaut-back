namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record ProductionLineResource(Guid Id, string Name, int Capacity, int MachineryCount);
