using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.Subscription.Domain.Services;
using AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Transform;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace AwawaTech.Mecanaut.API.Subscription.Interfaces.REST;
[ApiController]
[Route("api/v1/subscription-plans")]
public class SubscriptionPlansController : ControllerBase
{
    private readonly ISubscriptionPlanCommandService _cmd;
    private readonly ISubscriptionPlanQueryService _qry;

    public SubscriptionPlansController(
        ISubscriptionPlanCommandService cmd,
        ISubscriptionPlanQueryService qry)
    {
        _cmd = cmd;
        _qry = qry;
    }

    /* ---------- CREATE ---------- */
    [HttpPost]
    public async Task<ActionResult<SubscriptionPlanResource>> Create([FromBody] CreateSubscriptionPlanResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);  // Validación del modelo
        }

        var cmd = CreateSubscriptionPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var id = await _cmd.HandleAsync(cmd);

        var plan = await _qry.HandleAsync(new GetSubscriptionPlanByIdQuery { Id = id });
        var resourceResult = SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity(plan);  // Cambié el nombre de la variable

        return CreatedAtAction(nameof(GetById), new { id }, resourceResult);  // Regresa el recurso creado
    }

    /* ---------- READ ALL ---------- */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubscriptionPlanResource>>> GetAll()
    {
        var plans = await _qry.HandleAsync(new GetAllSubscriptionPlansQuery());
        var resources = plans.Select(SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /* ---------- READ ONE ---------- */
    [HttpGet("{id:long}")]
    public async Task<ActionResult<SubscriptionPlanResource>> GetById(long id)
    {
        var plan = await _qry.HandleAsync(new GetSubscriptionPlanByIdQuery { Id = id });
        if (plan == null) return NotFound();
        return Ok(SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity(plan));
    }

    /* ---------- UPDATE ---------- */
    [HttpPut("{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateSubscriptionPlanResource resource)
    {
        var cmd = UpdateSubscriptionPlanCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        await _cmd.HandleAsync(cmd);
        return NoContent();
    }

    /* ---------- CHANGE STATUS ---------- */
    [HttpPut("{id:long}/status")]
    public async Task<ActionResult> ChangeStatus(long id, [FromBody] string status)
    {
        var statusEnum = SubscriptionStatus.FromString(status);  // Convierte el string a SubscriptionStatus
        var cmd = new ChangeSubscriptionPlanStatusCommand { Id = id, Status = statusEnum };
        await _cmd.HandleAsync(cmd);
        return NoContent();
    }
}
