using System;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Events;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates
{
    public class PurchaseOrder : AuditableAggregateRoot
    {
        public string OrderNumber { get; private set; } = null!;
        public OrderStatus Status { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime? DeliveryDate { get; private set; }
        public long InventoryPartId { get; private set; }
        public int Quantity { get; private set; }
        public Money TotalPrice { get; private set; }
        public long PlantId { get; private set; }

        private PurchaseOrder(string orderNumber, long inventoryPartId, int quantity, 
             Money totalPrice, long plantId, DateTime? deliveryDate = null)
        {
            ValidateOrderNumber(orderNumber);
            ValidateQuantity(quantity);
            if (deliveryDate.HasValue)
                ValidateDeliveryDate(deliveryDate.Value);

            OrderNumber = orderNumber;
            Status = OrderStatus.Created;
            OrderDate = DateTime.UtcNow;
            InventoryPartId = inventoryPartId;
            Quantity = quantity;
            TotalPrice = totalPrice;
            PlantId = plantId;
            DeliveryDate = deliveryDate;

            AddDomainEvent(new PurchaseOrderCreatedEvent(Id, plantId, quantity));
        }

        protected PurchaseOrder() { }

        public static PurchaseOrder Create(string orderNumber, long inventoryPartId, 
            int quantity, Money totalPrice, long plantId, DateTime? deliveryDate = null)
            => new(orderNumber, inventoryPartId, quantity, totalPrice, plantId, deliveryDate);

        /* ---- Behaviour ---- */
        public void Complete(DateTime deliveryDate)
        {
            if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Cannot complete an order that is already completed or cancelled");

            Status = OrderStatus.Completed;
            DeliveryDate = deliveryDate;
            AddDomainEvent(new PurchaseOrderCompletedEvent(Id));
        }

        public void Cancel(string reason)
        {
            if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Cannot cancel completed or already cancelled orders");

            Status = OrderStatus.Cancelled;
            AddDomainEvent(new PurchaseOrderCancelledEvent(Id, reason));
        }

        /* ---- Validation ---- */
        private static void ValidateOrderNumber(string orderNumber)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
                throw new ArgumentException("Order number is required");
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");
        }

        private static void ValidateDeliveryDate(DateTime deliveryDate)
        {
            if (deliveryDate < DateTime.UtcNow)
                throw new ArgumentException("Delivery date must be in the future");
        }
    }
} 