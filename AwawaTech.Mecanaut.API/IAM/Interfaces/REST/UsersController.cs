using System.Net.Mime;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Transform;
using AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST;

/**
 * <summary>
 *     The user's controller
 * </summary>
 * <remarks>
 *     This class is used to handle user requests
 * </remarks>
 */
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(IUserQueryService userQueryService, IUserCommandService userCommandService) : ControllerBase
{
    /**
     * <summary>
     *     Get user by id endpoint. It allows to get a user by id
     * </summary>
     * <param name="id">The user id</param>
     * <returns>The user resource</returns>
     */
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a user by its id",
        Description = "Get a user by its id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserResource))]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user == null)
        {
            return NotFound();
        }
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    /**
     * <summary>
     *     Get all users' endpoint. It allows getting all users
     * </summary>
     * <returns>The user resources</returns>
     */
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Get all users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [SwaggerOperation(Summary="Create user",OperationId="CreateUser")]
    public async Task<IActionResult> Create([FromBody] CreateUserResource resource)
    {
        var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var user = await userCommandService.Handle(command);
        var userRes = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Created($"/api/v1/users/{user.Id}", userRes);
    }

    [Authorize(Policy="AdminOnly")]
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary="Update user",OperationId="UpdateUser")]
    public async Task<IActionResult> Update(int id,[FromBody] UpdateUserResource resource)
    {
        var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(id,resource);
        var user = await userCommandService.Handle(command);
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(user));
    }

    [Authorize(Policy="AdminOnly")]
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary="Delete user",OperationId="DeleteUser")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);
        var user = await userCommandService.Handle(command);
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(user));
    }
}