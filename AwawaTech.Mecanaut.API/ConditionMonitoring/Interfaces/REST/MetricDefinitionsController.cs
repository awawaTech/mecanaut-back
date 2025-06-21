using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Interfaces.REST;

[ApiController]
[Route("api/v1/metric-definitions")]
public class MetricDefinitionsController : ControllerBase
{
    private readonly IMetricQueryService metricQry;

    public MetricDefinitionsController(IMetricQueryService metricQry)
    {
        this.metricQry = metricQry;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MetricDefinitionResource>>> GetAll()
    {
        var defs = await metricQry.Handle(new GetAllMetricDefinitionsQuery());
        var res = defs.Select(MetricDefinitionResourceFromEntityAssembler.ToResource);
        return Ok(res);
    }
} 