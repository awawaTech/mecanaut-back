using System;
using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Events
{
    public record InventoryPartCreatedEvent(long Id, string PartNumber, long PlantId) : IDomainEvent;
} 