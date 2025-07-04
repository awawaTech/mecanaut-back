using System;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Application.Internal.CommandServices
{
    public class InventoryPartCommandService : IInventoryPartCommandService
    {
        private readonly IInventoryPartRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryPartCommandService(IInventoryPartRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<InventoryPart> Handle(CreateInventoryPartCommand command)
        {
            var part = InventoryPart.Create(
                command.Code,
                command.Name,
                command.Description,
                command.CurrentStock,
                command.MinStock,
                command.PlantId,
                new Money(command.UnitPrice)    
            );

            await _repository.AddAsync(part);
            await _unitOfWork.CompleteAsync();

            return part;
        }

        public async Task<InventoryPart> Handle(UpdateInventoryPartCommand command)
        {
            // Implementación
            throw new System.NotImplementedException();
        }
    }
} 