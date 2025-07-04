using System;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands
{
    public record UpdateInventoryPartCommand(
        Guid Id,
        string Name,
        string Description,
        int MinStock,
        decimal UnitPrice
    );
} 