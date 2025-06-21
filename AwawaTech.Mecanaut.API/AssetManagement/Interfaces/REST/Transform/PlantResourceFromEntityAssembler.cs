using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;

public static class PlantResourceFromEntityAssembler
{
    public static PlantResource ToResourceFromEntity(Plant entity)
    {
        return new PlantResource(
            entity.Id,
            entity.Name,
            entity.Location.Address,
            entity.Location.City,
            entity.Location.Country,
            entity.ContactInfo.Phone,
            entity.ContactInfo.Email,
            entity.Active);
    }
} 