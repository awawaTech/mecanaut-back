using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Transform;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/machines")]
public class MachinesController : ControllerBase
{
    private readonly IMachineCommandService _cmd;
    private readonly IMachineQueryService   _qry;
    private readonly IMachineMetricsCommandService _machineMetricsService;

    public MachinesController(IMachineCommandService cmd, IMachineQueryService qry, IMachineMetricsCommandService machineMetricsService)
    {
        _cmd = cmd; _qry = qry;
        _machineMetricsService = machineMetricsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MachineResource>>> GetAll()
    {
        var list = await _qry.Handle(new GetAllMachinesQuery());
        return Ok(list.Select(MachineResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<MachineResource>>> GetAvailable()
    {
        var list = await _qry.Handle(new GetAvailableMachinesQuery());
        return Ok(list.Select(MachineResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("maintenance-due")]
    public async Task<ActionResult<IEnumerable<MachineResource>>> GetMaintenanceDue()
    {
        var list = await _qry.Handle(new GetMachinesDueForMaintenanceQuery());
        return Ok(list.Select(MachineResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<MachineResource>> GetById(long id)
    {
        var machine = await _qry.Handle(new GetMachineByIdQuery(id));
        if (machine is null) return NotFound();
        return Ok(MachineResourceFromEntityAssembler.ToResourceFromEntity(machine));
    }

    [HttpPost]
    public async Task<ActionResult<MachineResource>> Register([FromBody] RegisterMachineResource resource)
    {
        var cmd = RegisterMachineCommandFromResourceAssembler.ToCommandFromResource(resource);
        var machine = await _cmd.Handle(cmd);
        
        foreach (var metric in resource.Metrics)
        {
            // Creamos el comando RecordMetricCommand con los valores necesarios
            var recordMetricCommand = new RecordMetricCommand(
                machine.Id, 
                metric.MetricId, 
                metric.Value, 
                metric.MeasuredAt ?? DateTime.UtcNow
            );

            // Llamamos directamente al servicio de MachineMetrics para registrar la métrica
            await _machineMetricsService.Handle(recordMetricCommand); 
        }
        
        return CreatedAtAction(nameof(GetById), new { id = machine.Id }, MachineResourceFromEntityAssembler.ToResourceFromEntity(machine));
    }

    [HttpPut("{machineId:long}/assign")]
    public async Task<ActionResult<MachineResource>> Assign(long machineId, [FromBody] AssignMachineResource resource)
    {
        var cmd = AssignMachineCommandFromResourceAssembler.ToCommandFromResource(machineId, resource);
        var machine = await _cmd.Handle(cmd);
        return Ok(MachineResourceFromEntityAssembler.ToResourceFromEntity(machine));
    }

    [HttpPut("{machineId:long}/maintenance/start")]
    public async Task<IActionResult> StartMaintenance(long machineId)
    {
        await _cmd.Handle(new StartMachineMaintenanceCommand(machineId));
        return NoContent();
    }

    [HttpPut("{machineId:long}/maintenance/complete")]
    public async Task<IActionResult> CompleteMaintenance(long machineId)
    {
        await _cmd.Handle(new CompleteMachineMaintenanceCommand(machineId));
        return NoContent();
    }

    [HttpGet("production-line/{lineId:long}")]
    public async Task<ActionResult<IEnumerable<MachineResource>>> GetByProductionLine(long lineId)
    {
        var list = await _qry.Handle(new GetMachinesByProductionLineQuery(lineId));
        return Ok(list.Select(MachineResourceFromEntityAssembler.ToResourceFromEntity));
    }
    
    [HttpGet("plant/{plantId:long}")]
    public async Task<ActionResult<IEnumerable<MachineResource>>> GetByPlantId(long plantId)
    {
        var list = await _qry.Handle(new GetMachinesByPlantIdQuery(plantId));
        return Ok(list.Select(MachineResourceFromEntityAssembler.ToResourceFromEntity));
    }
} 