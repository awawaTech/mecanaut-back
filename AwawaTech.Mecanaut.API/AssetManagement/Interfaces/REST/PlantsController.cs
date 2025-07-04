using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/plants")]
public class PlantsController : ControllerBase
{
    private readonly IPlantCommandService _commandService;
    private readonly IPlantQueryService   _queryService;

    public PlantsController(IPlantCommandService command, IPlantQueryService query)
    {
        _commandService = command;
        _queryService   = query;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlantResource>>> GetAll()
    {
        var plants = await _queryService.Handle(new GetAllPlantsQuery());
        return Ok(plants.Select(PlantResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<PlantResource>> GetById(long id)
    {
        var plant = await _queryService.Handle(new GetPlantByIdQuery(id));
        if (plant is null) return NotFound();
        return Ok(PlantResourceFromEntityAssembler.ToResourceFromEntity(plant));
    }

    [HttpPost]
    public async Task<ActionResult<PlantResource>> Create([FromBody] CreatePlantResource resource)
    {
        var command = CreatePlantCommandFromResourceAssembler.ToCommandFromResource(resource);
        var plant = await _commandService.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id = plant.Id }, PlantResourceFromEntityAssembler.ToResourceFromEntity(plant));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<PlantResource>> Update(long id, [FromBody] UpdatePlantResource resource)
    {
        var command = UpdatePlantCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var plant   = await _commandService.Handle(command);
        return Ok(PlantResourceFromEntityAssembler.ToResourceFromEntity(plant));
    }

    [HttpPut("{id:long}/activate")]
    public async Task<IActionResult> Activate(long id)
    {
        await _commandService.Handle(new ActivatePlantCommand(id));
        return NoContent();
    }

    [HttpPut("{id:long}/deactivate")]
    public async Task<IActionResult> Deactivate(long id)
    {
        await _commandService.Handle(new DeactivatePlantCommand(id));
        return NoContent();
    }
} 