using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Transform;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Interfaces.ASP.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderCommandService _commandService;
        private readonly IPurchaseOrderQueryService _queryService;
        private readonly IPurchaseOrderResourceAssembler _resourceAssembler;

        public PurchaseOrdersController(
            IPurchaseOrderCommandService commandService,
            IPurchaseOrderQueryService queryService,
            IPurchaseOrderResourceAssembler resourceAssembler)
        {
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
            _resourceAssembler = resourceAssembler ?? throw new ArgumentNullException(nameof(resourceAssembler));
        }

        /// <summary>
        /// Obtiene todas las órdenes de compra
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PurchaseOrderResource>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] int plantId)
        {
            var orders = await _queryService.Handle(new GetAllPurchaseOrdersQuery(plantId));
            var resources = _resourceAssembler.ToResource(orders);
            return Ok(resources);
        }

        /// <summary>
        /// Obtiene una orden de compra por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseOrderResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseOrderResource>> GetPurchaseOrderById(long id)
        {
            var order = await _queryService.Handle(new GetPurchaseOrderByIdQuery(id));
            if (order == null) return NotFound();
            return Ok(_resourceAssembler.ToResource(order));
        }

        /// <summary>
        /// Crea una nueva orden de compra
        /// </summary>
        /// 
        /// 
        
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseOrderResource), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PurchaseOrderResource>> CreatePurchaseOrder(
            [FromBody] CreatePurchaseOrderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            var command = new CreatePurchaseOrderCommand(
                resource.OrderNumber,
                resource.InventoryPartId,
                resource.Quantity,
                resource.TotalPrice,
                resource.PlantId,
                resource.DeliveryDate
            );

            var result = await _commandService.Handle(command);
            var responseResource = _resourceAssembler.ToResource(result);
            return CreatedAtAction(nameof(GetPurchaseOrderById), new { id = responseResource.Id }, responseResource);
        }

        /// <summary>
        /// Marca una orden de compra como completada
        /// </summary>
        [HttpPatch("{id}/complete")]
        [ProducesResponseType(typeof(PurchaseOrderResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseOrderResource>> CompletePurchaseOrder(long id)
        {
            try
            {
                var result = await _commandService.Handle(new CompletePurchaseOrderCommand(id));
                return Ok(_resourceAssembler.ToResource(result));
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina una orden de compra por su ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePurchaseOrder(long id)
        {
            var order = await _queryService.Handle(new GetPurchaseOrderByIdQuery(id));
            if (order == null)
                return NotFound(new { message = $"PurchaseOrder with ID {id} not found." });

            await _commandService.Handle(new DeletePurchaseOrderCommand(id));
            return NoContent();
        }
    }
} 