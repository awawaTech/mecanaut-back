using System;
using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources
{
    public class CreateInventoryPartResource
    {
        [Required]
        public string Code { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public int CurrentStock { get; set; }
        
        [Required]
        public int MinStock { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        [Required]
        public long PlantId { get; set; }
    }
} 