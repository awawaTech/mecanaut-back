using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/production-lines")]
public class ProductionLinesController : ControllerBase
{
    private readonly IProductionLineCommandService _cmdService;
    private readonly IProductionLineQueryService   _qryService;

    public ProductionLinesController(IProductionLineCommandService cmd, IProductionLineQueryService qry)
    {
        _cmdService = cmd;
        _qryService = qry;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductionLineResource>>> Get([FromQuery] long? plantId)
    {
        IEnumerable<Domain.Model.Aggregates.ProductionLine> lines;
        if (plantId.HasValue)
            lines = await _qryService.Handle(new GetProductionLinesByPlantQuery(plantId.Value));
        else
            lines = await _qryService.Handle(new GetRunningProductionLinesQuery()); // default all running
        return Ok(lines.Select(ProductionLineResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("running")]
    public async Task<ActionResult<IEnumerable<ProductionLineResource>>> GetRunning()
    {
        var lines = await _qryService.Handle(new GetRunningProductionLinesQuery());
        return Ok(lines.Select(ProductionLineResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("plant/{plantId:long}")]
    public async Task<ActionResult<IEnumerable<ProductionLineResource>>> GetByPlant(long plantId)
    {
        var lines = await _qryService.Handle(new GetProductionLinesByPlantQuery(plantId));
        return Ok(lines.Select(ProductionLineResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductionLineResource>> GetById(long id)
    {
        var line = await _qryService.Handle(new GetProductionLineByIdQuery(id));
        if (line is null) return NotFound();
        return Ok(ProductionLineResourceFromEntityAssembler.ToResourceFromEntity(line));
    }

    [HttpPost]
    public async Task<ActionResult<ProductionLineResource>> Create([FromBody] CreateProductionLineResource resource)
    {
        var cmd = CreateProductionLineCommandFromResourceAssembler.ToCommandFromResource(resource);
        var line = await _cmdService.Handle(cmd);
        return CreatedAtAction(nameof(GetById), new { id = line.Id }, ProductionLineResourceFromEntityAssembler.ToResourceFromEntity(line));
    }

    [HttpPut("{id:long}/start")]
    public async Task<IActionResult> Start(long id)
    {
        await _cmdService.Handle(new StartProductionCommand(id));
        return NoContent();
    }

    [HttpPut("{id:long}/stop")]
    public async Task<IActionResult> Stop(long id, [FromBody] StopProductionResource resource)
    {
        var cmd = StopProductionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        await _cmdService.Handle(cmd);
        return NoContent();
    }


}