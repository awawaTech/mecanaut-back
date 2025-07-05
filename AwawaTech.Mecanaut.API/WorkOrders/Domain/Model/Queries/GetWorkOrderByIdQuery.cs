using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Queries;

public class GetWorkOrderByIdQuery
{
    public long Id { get; set; }
    public TenantId TenantId { get; set; }
} 