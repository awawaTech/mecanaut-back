using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Application.Internal.QueryServices
{
    public class InventoryPartQueryService : IInventoryPartQueryService
    {
        private readonly IInventoryPartRepository _repository;

        public InventoryPartQueryService(IInventoryPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InventoryPart>> Handle(GetAllInventoryPartsQuery query)
        {
            return await _repository.FindByPlantId(query.PlantId);
        }

        public async Task<InventoryPart> Handle(GetInventoryPartByIdQuery query)
        {
            return await _repository.FindByIdAsync(query.Id);
        }

        public async Task<IEnumerable<InventoryPart>> Handle(GetInventoryPartsByPlantIdQuery query)
        {
            return await _repository.FindByPlantId(query.PlantId);
        }
    }
} 