using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/workAreas")]
public class WorkAreasController(
    IWorkAreaCommandService workAreaCommandService,
    IWorkAreaQueryService workAreaQueryService)
    : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllWorkAreas(
        CancellationToken cancellationToken)
    {
        var workAreas = await workAreaQueryService.Handle(
            new GetAllWorkAreasQuery(),
            cancellationToken);

        var resources = workAreas
            .Select(WorkAreaResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetWorkAreaById(
        int id,
        CancellationToken cancellationToken)
    {
        var workArea = await workAreaQueryService.Handle(
            new GetWorkAreaByIdQuery(id),
            cancellationToken);

        if (workArea is null)
            return NotFound();

        var resource = WorkAreaResourceFromEntityAssembler.ToResourceFromEntity(workArea);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkArea(
        [FromBody] CreateWorkAreaResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateWorkAreaCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await workAreaCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ShiftCoordinationActionResultAssembler.ToActionResult(result);

        var workAreaResource = WorkAreaResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return CreatedAtAction(
            nameof(GetWorkAreaById),
            new { id = workAreaResource.Id },
            workAreaResource);
    }
}