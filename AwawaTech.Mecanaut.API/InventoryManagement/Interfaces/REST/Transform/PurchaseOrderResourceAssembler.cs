using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Interfaces.ASP.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Transform
{
    public class PurchaseOrderResourceAssembler : IPurchaseOrderResourceAssembler
    {
        public PurchaseOrderResource ToResource(PurchaseOrder entity)
        {
            return new PurchaseOrderResource
            {
                Id = entity.Id,
                OrderNumber = entity.OrderNumber,
                InventoryPartId = entity.InventoryPartId,
                Quantity = entity.Quantity,
                TotalPrice = entity.TotalPrice.Amount,
                Status = entity.Status.ToString(),
                OrderDate = entity.OrderDate,
                DeliveryDate = entity.DeliveryDate,
                PlantId = entity.PlantId
            };
        }

        public IEnumerable<PurchaseOrderResource> ToResource(IEnumerable<PurchaseOrder> entities)
        {
            return entities.Select(ToResource);
        }
    }
} 