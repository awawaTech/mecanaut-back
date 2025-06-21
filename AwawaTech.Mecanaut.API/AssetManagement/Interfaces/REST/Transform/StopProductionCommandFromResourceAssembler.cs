using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class StopProductionCommandFromResourceAssembler
{
    public static StopProductionCommand ToCommandFromResource(long lineId, StopProductionResource resource)
        => new(lineId, resource.Reason);
} 