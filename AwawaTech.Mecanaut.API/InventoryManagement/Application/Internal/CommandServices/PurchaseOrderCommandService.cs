using System;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Application.Internal.CommandServices
{
    public class PurchaseOrderCommandService : IPurchaseOrderCommandService
    {
        private readonly IPurchaseOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderCommandService(IPurchaseOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PurchaseOrder> Handle(CreatePurchaseOrderCommand command)
        {
            var order = PurchaseOrder.Create(
                command.OrderNumber,
                command.InventoryPartId,
                command.Quantity,
                new Money(command.TotalPrice),
                command.PlantId,
                command.DeliveryDate
            );

            await _repository.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }

        public async Task<PurchaseOrder> Handle(CompletePurchaseOrderCommand command)
        {
            var order = await _repository.FindByIdAsync(command.Id);
            if (order == null)
                throw new ArgumentException($"Purchase order with ID {command.Id} not found.");

            order.Complete(DateTime.UtcNow);
            await _unitOfWork.CompleteAsync();

            return order;
        }

        public async Task<PurchaseOrder> Handle(UpdatePurchaseOrderCommand command)
        {
            // Implementación
            throw new System.NotImplementedException();
        }

        public async Task<PurchaseOrder> Handle(DeletePurchaseOrderCommand command)
        {
            var order = await _repository.FindByIdAsync(command.Id);
            if (order == null)
                throw new ArgumentException($"PurchaseOrder with ID {command.Id} not found.");

            _repository.Remove(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }
    }
} 