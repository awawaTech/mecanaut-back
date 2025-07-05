using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Queries;
 
public class GetExecutedWorkOrderByIdQuery
{
    public long Id { get; set; }
    public TenantId TenantId { get; set; }
} 