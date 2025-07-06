using System.Collections.Generic;
using System.Linq;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Interfaces.REST.Resources;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Interfaces.REST.Transform;

public class ExecutedWorkOrderToResourceAssembler
{
    public static ExecutedWorkOrderResource ToResource(ExecutedWorkOrder executedWorkOrder, IEnumerable<UsedProduct> usedProducts,
        IEnumerable<string> executionImages)
    {
        return new ExecutedWorkOrderResource
        {
            Id = executedWorkOrder.Id,
            Code = executedWorkOrder.Code,
            ExecutionDate = executedWorkOrder.ExecutionDate,
            ProductionLineId = executedWorkOrder.ProductionLineId,
            IntervenedMachineIds = executedWorkOrder.IntervenedMachineIds,
            AssignedTechnicianIds = executedWorkOrder.AssignedTechnicianIds,
            ExecutedTasks = executedWorkOrder.ExecutedTasks,
            UsedProducts = usedProducts
                .Where(p => p.ExecutedWorkOrderId == executedWorkOrder.Id)
                .Select(p => new ProductQuantityResource
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList(),
            ExecutionImages = executionImages.ToList()
        };
    }
}