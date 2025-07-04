using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class UpdatePlantCommandFromResourceAssembler
{
    public static UpdatePlantCommand ToCommandFromResource(long plantId, UpdatePlantResource resource)
    {
        var location = new Location(resource.Address, resource.City, resource.Country);
        var contact  = new ContactInfo(resource.Phone, resource.Email);
        return new UpdatePlantCommand(plantId, resource.Name, location, contact);
    }
}