using System.Net.Mime;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Asset Management endpoints (plants, lines, machinery)")]
public class AssetManagementController(
    IPlantCommandService plantCmd,
    IPlantQueryService plantQry,
    IProductionLineCommandService lineCmd,
    IProductionLineQueryService lineQry,
    IMachineryCommandService machCmd,
    IMachineryQueryService machQry) : ControllerBase
{
    // ───────────── PLANTS ─────────────
    [HttpPost("plants")] 
    [SwaggerOperation("Create Plant", "Create a new plant", OperationId = "CreatePlant")] 
    [SwaggerResponse(201, "Plant created", typeof(PlantResource))]
    public async Task<IActionResult> CreatePlant([FromBody] CreatePlantResource body)
    {
        var cmd   = CreatePlantCommandFromResourceAssembler.ToCommand(body);
        var plant = await plantCmd.Handle(cmd);
        var res   = PlantResourceFromEntityAssembler.ToResource(plant);
        return CreatedAtAction(nameof(GetPlantById), new { plantId = plant.Id.Value }, res);
    }

    [HttpGet("plants/{plantId:guid}")]
    [SwaggerResponse(200, "Plant found", typeof(PlantResource))]
    [SwaggerResponse(404, "Plant not found")]
    public async Task<IActionResult> GetPlantById(Guid plantId)
    {
        var plant = await plantQry.Handle(new GetPlantByIdQuery(new PlantId(plantId)));
        if (plant is null) return NotFound();
        return Ok(PlantResourceFromEntityAssembler.ToResource(plant));
    }

    [HttpGet("plants")]
    [SwaggerResponse(200, "All plants", typeof(IEnumerable<PlantResource>))]
    public async Task<IActionResult> GetAllPlants()
    {
        var list = await plantQry.Handle(new GetAllPlantsQuery());
        return Ok(list.Select(PlantResourceFromEntityAssembler.ToResource));
    }

    // ───────────── LINES ─────────────
    [HttpPost("plants/{plantId:guid}/lines")]
    [SwaggerResponse(201, "Line created", typeof(ProductionLineResource))]
    public async Task<IActionResult> AddLine(Guid plantId, [FromBody] AddLineResource body)
    {
        var cmd  = AddLineCommandFromResourceAssembler.ToCommand(plantId, body);
        var line = await lineCmd.Handle(cmd);
        if (line is null) return NotFound();
        var res  = LineResourceFromEntityAssembler.ToResource(line);
        return Created(string.Empty, res);
    }

    [HttpGet("plants/{plantId:guid}/lines")]
    [SwaggerResponse(200, "Lines", typeof(IEnumerable<ProductionLineResource>))]
    public async Task<IActionResult> GetLines(Guid plantId)
    {
        var list = await lineQry.Handle(new GetProductionLinesByPlantQuery(new PlantId(plantId)));
        return Ok(list.Select(LineResourceFromEntityAssembler.ToResource));
    }

    // ───────────── MACHINERY ─────────────
    [HttpPost("lines/{lineId:guid}/machinery")]
    [SwaggerResponse(201, "Machinery created", typeof(MachineryResource))]
    public async Task<IActionResult> AddMachinery(Guid lineId, [FromBody] CreateMachineryResource body)
    {
        var cmd  = CreateMachineryCommandFromResourceAssembler.ToCommand(lineId, body);
        var mach = await machCmd.Handle(cmd);
        if (mach is null) return NotFound();
        var res  = MachineryResourceFromEntityAssembler.ToResource(mach);
        return Created(string.Empty, res);
    }

    [HttpGet("lines/{lineId:guid}/machinery")]
    [SwaggerResponse(200, "Machinery list", typeof(IEnumerable<MachineryResource>))]
    public async Task<IActionResult> GetMachinery(Guid lineId)
    {
        var list = await machQry.Handle(new GetMachineryByLineQuery(new ProductionLineId(lineId)));
        return Ok(list.Select(MachineryResourceFromEntityAssembler.ToResource));
    }
}