using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Queries;

public class GetWorkOrdersByProductionLineQuery
{
    public long ProductionLineId { get; set; }
    public TenantId TenantId { get; set; }
} 