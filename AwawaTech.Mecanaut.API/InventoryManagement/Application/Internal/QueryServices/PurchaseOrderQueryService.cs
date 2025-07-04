using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Application.Internal.QueryServices
{
    public class PurchaseOrderQueryService : IPurchaseOrderQueryService
    {
        private readonly IPurchaseOrderRepository _repository;

        public PurchaseOrderQueryService(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PurchaseOrder>> Handle(GetAllPurchaseOrdersQuery query)
        {
            return await _repository.FindByPlantIdAsync(query.PlantId);
        }

        public async Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query)
        {
            return await _repository.FindByIdAsync(query.Id);
        }
    }
} 