namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record PlantResource(
    long Id,
    string Name,
    string Address,
    string City,
    string Country,
    string Phone,
    string Email,
    bool Active); 