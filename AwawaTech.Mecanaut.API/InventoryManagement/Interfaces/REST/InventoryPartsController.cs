using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Services;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Interfaces.REST
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class InventoryPartsController : ControllerBase
    {
        private readonly IInventoryPartCommandService _commandService;
        private readonly IInventoryPartQueryService _queryService;
        private readonly IInventoryPartResourceAssembler _resourceAssembler;

        public InventoryPartsController(
            IInventoryPartCommandService commandService,
            IInventoryPartQueryService queryService,
            IInventoryPartResourceAssembler resourceAssembler)
        {
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
            _resourceAssembler = resourceAssembler ?? throw new ArgumentNullException(nameof(resourceAssembler));
        }

        /// <summary>
        /// Obtiene todas las partes del inventario
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InventoryPartResource>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] int plantId)
        {
            var parts = await _queryService.Handle(new GetAllInventoryPartsQuery(plantId));
            var resources = _resourceAssembler.ToResource(parts);
            return Ok(resources);
        }

        /// <summary>
        /// Obtiene una parte del inventario por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InventoryPartResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryPartResource>> GetInventoryPartById(string id)
        {
            // Intentar primero como GUID
            if (Guid.TryParse(id, out var guidId))
            {
                var part = await _queryService.Handle(new GetInventoryPartByIdQuery(guidId));
                if (part != null)
                {
                    return Ok(_resourceAssembler.ToResource(part));
                }
            }

            // Si no es GUID o no se encontró, intentar como PlantId
            if (long.TryParse(id, out var plantId))
            {
                var parts = await _queryService.Handle(new GetInventoryPartsByPlantIdQuery(plantId));
                var resources = _resourceAssembler.ToResource(parts);
                return Ok(resources);
            }

            return BadRequest(new { message = "Invalid ID format. Must be either a GUID or a numeric Plant ID." });
        }

        /// <summary>
        /// Crea una nueva parte del inventario
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(InventoryPartResource), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryPartResource>> CreateInventoryPart([FromBody] CreateInventoryPartResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateInventoryPartCommand(
                resource.Code,
                resource.Name,
                resource.Description,
                resource.CurrentStock,
                resource.MinStock,
                resource.UnitPrice,
                resource.PlantId);

            var result = await _commandService.Handle(command);
            var resourceResult = _resourceAssembler.ToResource(result);
            return CreatedAtAction(nameof(GetInventoryPartById), new { id = resourceResult.Id }, resourceResult);
        }

        /// <summary>
        /// Obtiene todas las partes del inventario por PlantId
        /// </summary>
        [HttpGet("by-plant/{plantId}")]
        [ProducesResponseType(typeof(IEnumerable<InventoryPartResource>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InventoryPartResource>>> GetByPlantId(long plantId)
        {
            var parts = await _queryService.Handle(new GetInventoryPartsByPlantIdQuery(plantId));
            var resources = _resourceAssembler.ToResource(parts);
            return Ok(resources);
        }
    }
} 