using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.WorkOrders.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.WorkOrders.Domain.Services;

public interface IWorkOrderQueryService
{
    Task<WorkOrder> Handle(GetWorkOrderByIdQuery query);
    Task<IEnumerable<WorkOrder>> Handle(GetWorkOrdersByProductionLineQuery query);

    Task<IEnumerable<WorkOrder>> GetTo(GetWorkOrdersByProductionLineQuery query);
} 