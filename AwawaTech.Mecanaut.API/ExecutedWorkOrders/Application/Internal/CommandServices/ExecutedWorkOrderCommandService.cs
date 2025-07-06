using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Repositories;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Application.Internal.OutboundServices;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Application.Internal.CommandServices;

public class ExecutedWorkOrderCommandService : IExecutedWorkOrderCommandService
{
    private readonly IExecutedWorkOrderRepository _repository;
    private readonly TenantContextHelper tenantHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInventoryManagementAcl _inventoryAcl;

    public ExecutedWorkOrderCommandService(IExecutedWorkOrderRepository repository, IUnitOfWork unitOfWork
    ,TenantContextHelper tenantHelper, IInventoryManagementAcl inventoryAcl)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        this.tenantHelper = tenantHelper;
        _inventoryAcl = inventoryAcl;
    }

    public async Task<ExecutedWorkOrder> HandleAsync(CreateExecutedWorkOrderCommand command)
    {
        var tenantId = new TenantId(tenantHelper.GetCurrentTenantId());
        
        var executedWorkOrder = ExecutedWorkOrder.Create(
            command.Code,
            tenantId,
            command.ExecutionDate,
            command.ProductionLineId);

        executedWorkOrder.SetIntervenedMachines(command.IntervenedMachineIds);
        executedWorkOrder.SetAssignedTechnicians(command.AssignedTechnicianIds);
        executedWorkOrder.SetExecutedTasks(command.ExecutedTasks);

        await _repository.AddAsync(executedWorkOrder);
        await _unitOfWork.CompleteAsync(); // 🔑 Esto sí persiste y genera el ID

        foreach (var (productId, quantity) in command.UsedProducts)
        {
            var usedProduct = new UsedProduct(executedWorkOrder.Id, productId, quantity);
            await _repository.AddEntityAsync(usedProduct);
        }

        await _unitOfWork.CompleteAsync();
        
        foreach (var part in command.UsedProducts)
        {
            await _inventoryAcl.DecreaseInventoryQuantityAsync(
                part.ProductId,
                part.Quantity
            );
        }
        
        return executedWorkOrder;
    }
}