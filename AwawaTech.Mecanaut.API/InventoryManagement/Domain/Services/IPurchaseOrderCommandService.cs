using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services
{
    public interface IPurchaseOrderCommandService
    {
        Task<PurchaseOrder> Handle(CreatePurchaseOrderCommand command);
        Task<PurchaseOrder> Handle(UpdatePurchaseOrderCommand command);
    }
} 