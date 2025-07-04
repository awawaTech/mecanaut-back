namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;

public record CreateInventoryPartCommand(
    string Code,
    string Name,
    string Description,
    int CurrentStock,
    int MinStock,
    decimal UnitPrice,
    long PlantId
); 