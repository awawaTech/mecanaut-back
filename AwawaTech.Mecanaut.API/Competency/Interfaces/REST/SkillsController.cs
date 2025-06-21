using AwawaTech.Mecanaut.API.Competency.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.Competency.Domain.Services;
using AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.Competency.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.Competency.Interfaces.REST;

[ApiController]
[Route("api/v1/skills")]
public class SkillsController : ControllerBase
{
    private readonly ISkillCommandService _cmd;
    private readonly ISkillQueryService   _qry;

    public SkillsController(ISkillCommandService cmd, ISkillQueryService qry)
    {
        _cmd = cmd;
        _qry = qry;
    }

    /* ---------- CREATE ---------- */
    [HttpPost]
    public async Task<ActionResult<SkillResource>> Create([FromBody] CreateSkillResource resource)
    {
        var cmd = CreateSkillCommandFromResourceAssembler.ToCommandFromResource(resource);
        var skill = await _cmd.Handle(cmd);
        return CreatedAtAction(nameof(GetById), new { skillId = skill.Id }, SkillResourceFromEntityAssembler.ToResourceFromEntity(skill));
    }

    /* ---------- READ ALL ---------- */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SkillResource>>> GetAll()
    {
        var list = await _qry.Handle(new GetAllSkillsQuery());
        return Ok(list.Select(SkillResourceFromEntityAssembler.ToResourceFromEntity));
    }

    /* ---------- READ ONE ---------- */
    [HttpGet("{skillId:long}")]
    public async Task<ActionResult<SkillResource>> GetById(long skillId)
    {
        var skill = await _qry.Handle(new GetSkillByIdQuery(skillId));
        if (skill is null) return NotFound();
        return Ok(SkillResourceFromEntityAssembler.ToResourceFromEntity(skill));
    }

    /* ---------- UPDATE ---------- */
    [HttpPut("{skillId:long}")]
    public async Task<ActionResult<SkillResource>> Update(long skillId, [FromBody] UpdateSkillResource resource)
    {
        var cmd = UpdateSkillCommandFromResourceAssembler.ToCommandFromResource(skillId, resource);
        var skill = await _cmd.Handle(cmd);
        return Ok(SkillResourceFromEntityAssembler.ToResourceFromEntity(skill));
    }

    /* ---------- DEACTIVATE ---------- */
    [HttpDelete("{skillId:long}")]
    public async Task<IActionResult> Deactivate(long skillId)
    {
        await _cmd.Handle(new DeactivateSkillCommand(skillId));
        return NoContent();
    }
} 