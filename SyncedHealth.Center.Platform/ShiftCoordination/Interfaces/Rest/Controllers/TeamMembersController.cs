using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/teamMembers")]
public class TeamMembersController(
    ITeamMemberCommandService teamMemberCommandService,
    ITeamMemberQueryService teamMemberQueryService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTeamMembers(
        [FromQuery] int? teamId,
        [FromQuery] int? userId,
        CancellationToken cancellationToken)
    {
        var teamMembers = teamId.HasValue
            ? await teamMemberQueryService.Handle(
                new GetTeamMembersByTeamIdQuery(teamId.Value),
                cancellationToken)
            : userId.HasValue
                ? await teamMemberQueryService.Handle(
                    new GetTeamMembersByUserIdQuery(userId.Value),
                    cancellationToken)
                : await teamMemberQueryService.Handle(
                    new GetAllTeamMembersQuery(),
                    cancellationToken);

        var resources = teamMembers
            .Select(TeamMemberResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTeamMemberById(
        int id,
        CancellationToken cancellationToken)
    {
        var teamMember = await teamMemberQueryService.Handle(
            new GetTeamMemberByIdQuery(id),
            cancellationToken);

        if (teamMember is null)
            return NotFound();

        var resource = TeamMemberResourceFromEntityAssembler.ToResourceFromEntity(teamMember);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeamMember(
        [FromBody] CreateTeamMemberResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateTeamMemberCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await teamMemberCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ShiftCoordinationActionResultAssembler.ToActionResult(result);

        var teamMemberResource = TeamMemberResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return CreatedAtAction(
            nameof(GetTeamMemberById),
            new { id = teamMemberResource.Id },
            teamMemberResource);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTeamMember(
        int id,
        CancellationToken cancellationToken)
    {
        var command = DeleteTeamMemberCommandFromRouteAssembler.ToCommandFromRoute(id);

        var result = await teamMemberCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ShiftCoordinationActionResultAssembler.ToActionResult(result);

        return NoContent();
    }
}