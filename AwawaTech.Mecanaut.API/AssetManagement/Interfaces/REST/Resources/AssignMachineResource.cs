using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record AssignMachineResource([Required] long ProductionLineId);