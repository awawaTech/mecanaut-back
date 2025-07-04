using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Queries;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services
{
    public interface IInventoryPartQueryService
    {
        Task<IEnumerable<InventoryPart>> Handle(GetAllInventoryPartsQuery query);
        Task<InventoryPart> Handle(GetInventoryPartByIdQuery query);
        Task<IEnumerable<InventoryPart>> Handle(GetInventoryPartsByPlantIdQuery query);
    }
} 