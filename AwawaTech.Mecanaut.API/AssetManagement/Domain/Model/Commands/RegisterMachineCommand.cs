using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;

public record RegisterMachineCommand(string SerialNumber, string Name, MachineSpecs Specs); 