using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services
{
    public interface IPurchaseOrderQueryService
    {
        Task<IEnumerable<PurchaseOrder>> Handle(GetAllPurchaseOrdersQuery query);
        Task<PurchaseOrder> Handle(GetPurchaseOrderByIdQuery query);
    }
} 