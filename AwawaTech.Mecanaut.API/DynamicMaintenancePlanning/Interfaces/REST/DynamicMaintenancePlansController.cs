using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Services;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST.Transform;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class DynamicMaintenancePlansController : ControllerBase
{
    private readonly IDynamicMaintenancePlanCommandService _commandService;
    private readonly IDynamicMaintenancePlanQueryService _queryService;
    private readonly DynamicMaintenancePlanToResourceAssembler _toResourceAssembler;
    private readonly DynamicMaintenancePlanWithDetailsToResourceAssembler _toResourceAssemblerWithDetails;
    
    private readonly SaveDynamicMaintenancePlanCommandFromResourceAssembler _fromResourceAssembler;

    public DynamicMaintenancePlansController(
        IDynamicMaintenancePlanCommandService commandService,
        IDynamicMaintenancePlanQueryService queryService,
        DynamicMaintenancePlanToResourceAssembler toResourceAssembler,
        DynamicMaintenancePlanWithDetailsToResourceAssembler toResourceAssemblerWithDetails,
        SaveDynamicMaintenancePlanCommandFromResourceAssembler fromResourceAssembler)
    {
        _commandService = commandService;
        _queryService = queryService;
        _toResourceAssembler = toResourceAssembler;
        _toResourceAssemblerWithDetails = toResourceAssemblerWithDetails;
        _fromResourceAssembler = fromResourceAssembler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DynamicMaintenancePlanWithDetailsResource>>> GetAllAsync([FromQuery] string plantLineId)
    {
        // Obtener el tenantId desde las claims del token JWT
        var tenantId = User.Claims.FirstOrDefault(c => c.Type == "tenant_id")?.Value;

        if (tenantId == null)
        {
            return Unauthorized("Tenant ID not found in token.");
        }

        // Crear la consulta con el tenantId y plantLineId
        var query = new GetAllDynamicMaintenancePlansQuery
        {
            TenantId = tenantId,
            PlantLineId = plantLineId // Pasar el plantLineId a la consulta
        };

        // Llamar al servicio para obtener los planes
        var plans = await _queryService.GetAllAsync(query);

        // Usar el ensamblador para transformar los datos en el formato adecuado
        var resources = plans.Select(p => _toResourceAssemblerWithDetails.ToResource(p));

        return Ok(resources);
    }


    [HttpGet("{id:long}")]
    public async Task<ActionResult<DynamicMaintenancePlanResource>> GetPlanByIdAsync(long id)
    {
        var plan = await _queryService.GetAsync(new GetDynamicMaintenancePlanQuery { Id = id.ToString() });

        if (plan == null)
            return NotFound();

        var resource = _toResourceAssembler.ToResource(plan);
        return Ok(resource);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<DynamicMaintenancePlanResource>> CreateAsync([FromBody] SaveDynamicMaintenancePlanResource resource)
    {
        if (resource == null)
            return BadRequest("Invalid body.");

        var command = _fromResourceAssembler.ToCommand(resource);
        var plan = await _commandService.CreateAsync(command);
        
        //Console.WriteLine($"Plan ID: {plan.Id}");
        
        var resultResource = _toResourceAssembler.ToResource(plan);

        Console.WriteLine($"Plan ID: {plan.Id}");
        // CreatedAtAction apunta al GetByIdAsync para devolver la URL correcta
        return Ok(resultResource);
    }
}
