using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

public record CreatePlantCommand(string Name, Location Location, ContactInfo ContactInfo); 