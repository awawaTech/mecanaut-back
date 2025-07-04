using System;
using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources
{
    public class CreatePurchaseOrderResource
    {
        [Required]
        public string OrderNumber { get; set; } = null!;
        
        [Required]
        public long InventoryPartId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        [Required]
        public decimal TotalPrice { get; set; }
        
        [Required]
        public long PlantId { get; set; }
        
        public DateTime? DeliveryDate { get; set; }
    }
} 