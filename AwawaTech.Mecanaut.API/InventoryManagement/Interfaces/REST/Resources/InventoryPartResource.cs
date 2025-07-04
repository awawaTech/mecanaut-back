using System;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources
{
    public class InventoryPartResource
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentStock { get; set; }
        public int MinStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string StockStatus { get; set; }
    }
} 