using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Services;
 
public interface IExecutedWorkOrderCommandService
{
    Task<ExecutedWorkOrder> HandleAsync(CreateExecutedWorkOrderCommand command);
} 