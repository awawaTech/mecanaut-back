using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Queries;

public class GetAllWorkOrdersQuery
{
    public TenantId TenantId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
} 