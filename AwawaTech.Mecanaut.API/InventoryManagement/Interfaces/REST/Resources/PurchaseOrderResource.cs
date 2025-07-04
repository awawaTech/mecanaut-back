using System;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources
{
    public class PurchaseOrderResource
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public long InventoryPartId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public long PlantId { get; set; }
    }
} 