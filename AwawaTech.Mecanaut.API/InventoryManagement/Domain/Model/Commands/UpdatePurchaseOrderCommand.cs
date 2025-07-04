using System;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands
{
    public record UpdatePurchaseOrderCommand(
        Guid Id,
        OrderStatus Status
    );
} 