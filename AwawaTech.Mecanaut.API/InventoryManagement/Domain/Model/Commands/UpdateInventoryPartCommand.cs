using System;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands
{
    public record UpdateInventoryPartCommand(
        long Id,
        string? Description = null,
        int? CurrentStock = null,
        int? MinStock = null,
        decimal? UnitPrice = null
    );
} 