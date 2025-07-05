namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

public class DecreaseInventoryCommand
{
    public long InventoryPartId { get; set; }
    public int Quantity { get; set; }
}