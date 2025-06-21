using System.Net.Mime;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;
using AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Transform;
using AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Roles endpoints")] 
public class RolesController(IRoleQueryService roleQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all roles", OperationId = "GetRoles")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAll()
    {
        var roles = await roleQueryService.Handle(new AwawaTech.Mecanaut.API.IAM.Domain.Model.Queries.GetAllRolesQuery());
        var resources = roles.Select(RoleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
} 