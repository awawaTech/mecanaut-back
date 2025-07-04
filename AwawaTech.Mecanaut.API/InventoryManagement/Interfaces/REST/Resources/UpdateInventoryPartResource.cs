using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources
{
    public class UpdateInventoryPartResource
    {
        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int? CurrentStock { get; set; }

        [Range(0, int.MaxValue)]
        public int? MinStock { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? UnitPrice { get; set; }
    }
} 