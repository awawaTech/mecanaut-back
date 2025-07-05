using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Queries;

public class GetWorkOrdersByStatusQuery
{
    public WorkOrderStatus Status { get; set; }
    public TenantId TenantId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
} 