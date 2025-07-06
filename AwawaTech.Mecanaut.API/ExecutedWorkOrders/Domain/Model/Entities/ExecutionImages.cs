
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;

public class ExecutionImages: AuditableEntity
{
    public long ExecutedWorkOrderId { get; private set; }
    public string ImageUrl { get; private set; }

    public ExecutionImages(long executedWorkOrderId, string imageUrl)
    {
        ExecutedWorkOrderId = executedWorkOrderId;
        ImageUrl = imageUrl;
    }
} 