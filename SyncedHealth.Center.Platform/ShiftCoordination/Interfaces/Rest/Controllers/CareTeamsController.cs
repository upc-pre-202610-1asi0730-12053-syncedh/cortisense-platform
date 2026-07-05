using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/careTeams")]
public class CareTeamsController(
    ICareTeamCommandService careTeamCommandService,
    ICareTeamQueryService careTeamQueryService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCareTeams(
        [FromQuery] int? organizationId,
        [FromQuery] int? supervisorId,
        [FromQuery] int? workAreaId,
        CancellationToken cancellationToken)
    {
        var careTeams = organizationId.HasValue
            ? await careTeamQueryService.Handle(
                new GetCareTeamsByOrganizationIdQuery(organizationId.Value),
                cancellationToken)
            : supervisorId.HasValue
                ? await careTeamQueryService.Handle(
                    new GetCareTeamsBySupervisorIdQuery(supervisorId.Value),
                    cancellationToken)
                : workAreaId.HasValue
                    ? await careTeamQueryService.Handle(
                        new GetCareTeamsByWorkAreaIdQuery(workAreaId.Value),
                        cancellationToken)
                    : await careTeamQueryService.Handle(
                        new GetAllCareTeamsQuery(),
                        cancellationToken);

        var resources = careTeams
            .Select(CareTeamResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCareTeamById(
        int id,
        CancellationToken cancellationToken)
    {
        var careTeam = await careTeamQueryService.Handle(
            new GetCareTeamByIdQuery(id),
            cancellationToken);

        if (careTeam is null)
            return NotFound();

        var resource = CareTeamResourceFromEntityAssembler.ToResourceFromEntity(careTeam);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCareTeam(
        [FromBody] CreateCareTeamResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateCareTeamCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await careTeamCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ShiftCoordinationActionResultAssembler.ToActionResult(result);

        var careTeamResource = CareTeamResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return CreatedAtAction(
            nameof(GetCareTeamById),
            new { id = careTeamResource.Id },
            careTeamResource);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateCareTeam(
        int id,
        [FromBody] UpdateCareTeamResource resource,
        CancellationToken cancellationToken)
    {
        var currentCareTeam = await careTeamQueryService.Handle(
            new GetCareTeamByIdQuery(id),
            cancellationToken);

        if (currentCareTeam is null)
            return NotFound();

        var command = UpdateCareTeamCommandFromResourceAssembler.ToCommandFromResource(
            id,
            resource,
            currentCareTeam);

        var result = await careTeamCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ShiftCoordinationActionResultAssembler.ToActionResult(result);

        var careTeamResource = CareTeamResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return Ok(careTeamResource);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCareTeam(
        int id,
        CancellationToken cancellationToken)
    {
        var command = DeleteCareTeamCommandFromRouteAssembler.ToCommandFromRoute(id);

        var result = await careTeamCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ShiftCoordinationActionResultAssembler.ToActionResult(result);

        return NoContent();
    }
}