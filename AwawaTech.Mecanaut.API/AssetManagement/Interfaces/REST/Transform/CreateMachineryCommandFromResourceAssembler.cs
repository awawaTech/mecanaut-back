using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class CreateMachineryCommandFromResourceAssembler
{
    public static CreateMachineryCommand ToCommand(Guid lineId, CreateMachineryResource resource)
        => new(new ProductionLineId(lineId), resource.Model, resource.Brand);
}
