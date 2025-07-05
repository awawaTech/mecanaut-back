using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;

public class UsedProduct : AuditableEntity
{
    public long ExecutedWorkOrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }

    public UsedProduct(long executedWorkOrderId, long productId, int quantity)
    {
        ExecutedWorkOrderId = executedWorkOrderId;
        ProductId = productId;
        Quantity = quantity;
    }
} 