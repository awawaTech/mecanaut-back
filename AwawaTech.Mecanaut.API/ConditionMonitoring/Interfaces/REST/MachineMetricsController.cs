using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST;

[ApiController]
[Route("api/v1/machines/{machineId:long}/metrics")]
public class MachineMetricsController : ControllerBase
{
    private readonly IMachineMetricsCommandService cmd;
    private readonly IMachineMetricsQueryService  qry;
    private readonly IMetricQueryService          metricQry;

    public MachineMetricsController(IMachineMetricsCommandService cmd,
                                    IMachineMetricsQueryService qry,
                                    IMetricQueryService metricQry)
    {
        this.cmd = cmd;
        this.qry = qry;
        this.metricQry = metricQry;
    }

    // POST register metric
    [HttpPost]
    public async Task<IActionResult> Record(long machineId, [FromBody] RecordMetricResource body)
    {
        var command = RecordMetricCommandFromResourceAssembler.ToCommand(machineId, body);
        await cmd.Handle(command);
        return StatusCode(StatusCodes.Status201Created);
    }

    // GET all current metrics
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrentMetricResource>>> GetCurrent(long machineId)
    {
        var current = await qry.Handle(new GetCurrentMetricsByMachineQuery(machineId));
        if (!current.Any()) return Ok(current); // empty
        var defs = await metricQry.Handle(new GetAllMetricDefinitionsQuery());
        var dictDefs = defs.ToDictionary(d => d.Id);
        var list = current.Select(e =>
            CurrentMetricResourceAssembler.ToResource(e.Key, e.Value, dictDefs[e.Key]));
        return Ok(list);
    }

    // GET specific current metric
    [HttpGet("{metricId:long}")]
    public async Task<ActionResult<CurrentMetricResource>> GetCurrentMetric(long machineId, long metricId)
    {
        var cm = await qry.Handle(new GetCurrentMetricQuery(machineId, metricId));
        if (cm is null) return NotFound();
        var def = (await metricQry.Handle(new GetAllMetricDefinitionsQuery())).First(d => d.Id == metricId);
        return Ok(CurrentMetricResourceAssembler.ToResource(metricId, cm, def));
    }

    // GET historical readings
    [HttpGet("{metricId:long}/readings")]
    public async Task<ActionResult<IEnumerable<MetricReadingResource>>> GetReadings(long machineId, long metricId,
        [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int page = 0, [FromQuery] int size = 20)
    {
        var list = await qry.Handle(new GetMetricReadingsQuery(machineId, metricId, from ?? DateTime.UnixEpoch, to ?? DateTime.UtcNow, page, size));
        var res = list.Select(MetricReadingResourceFromEntityAssembler.ToResource);
        return Ok(res);
    }
} 