using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services
{
    public interface IInventoryPartCommandService
    {
        Task<InventoryPart> Handle(CreateInventoryPartCommand command);
        Task<InventoryPart> Handle(UpdateInventoryPartCommand command);
        Task<InventoryPart> Handle(DeleteInventoryPartCommand command);

        Task HandleAsync(DecreaseInventoryCommand command);
    }
} 