using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

public record UpdatePlantCommand(long PlantId, string Name, Location Location, ContactInfo ContactInfo); 