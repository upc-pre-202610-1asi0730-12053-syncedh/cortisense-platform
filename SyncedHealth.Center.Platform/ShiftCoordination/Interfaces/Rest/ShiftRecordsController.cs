using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest;

[ApiController]
[Route("api/v1/shiftRecords")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Shift Record Endpoints.")]
public class ShiftRecordsController(
    IShiftRecordCommandService shiftRecordCommandService,
    IShiftRecordQueryService shiftRecordQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Shift Records", "Get all shift records or filter by organizationId/userId/workAreaId/status.")]
    public async Task<IActionResult> GetShiftRecords(
        [FromQuery] int? organizationId,
        [FromQuery] int? userId,
        [FromQuery] int? workAreaId,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var result = await shiftRecordQueryService.Handle(
                new GetShiftRecordsByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(result.Select(ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (userId.HasValue)
        {
            var result = await shiftRecordQueryService.Handle(
                new GetShiftRecordsByUserIdQuery(userId.Value),
                cancellationToken);

            return Ok(result.Select(ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (workAreaId.HasValue)
        {
            var result = await shiftRecordQueryService.Handle(
                new GetShiftRecordsByWorkAreaIdQuery(workAreaId.Value),
                cancellationToken);

            return Ok(result.Select(ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            var result = await shiftRecordQueryService.Handle(
                new GetShiftRecordsByStatusQuery(status),
                cancellationToken);

            return Ok(result.Select(ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var shiftRecords = await shiftRecordQueryService.Handle(
            new GetAllShiftRecordsQuery(),
            cancellationToken);

        return Ok(shiftRecords.Select(ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Shift Record by Id", "Get a shift record by its identifier.")]
    public async Task<IActionResult> GetShiftRecordById(
        int id,
        CancellationToken cancellationToken)
    {
        var shiftRecord = await shiftRecordQueryService.Handle(
            new GetShiftRecordByIdQuery(id),
            cancellationToken);

        if (shiftRecord is null)
            return NotFound();

        return Ok(ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity(shiftRecord));
    }

    [HttpPost]
    [SwaggerOperation("Create Shift Record", "Create a new shift record.")]
    public async Task<IActionResult> CreateShiftRecord(
        [FromBody] CreateShiftRecordResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateShiftRecordCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await shiftRecordCommandService.Handle(command, cancellationToken);

        return ShiftCoordinationActionResultAssembler.ToActionResultFromCreateShiftRecordResult(
            this,
            result,
            problemDetailsFactory,
            createdShiftRecord => CreatedAtAction(
                nameof(GetShiftRecordById),
                new { id = createdShiftRecord.Id },
                ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity(createdShiftRecord)
            )
        );
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation("Update Shift Record Status", "Update a shift record status.")]
    public async Task<IActionResult> UpdateShiftRecordStatus(
        int id,
        [FromBody] UpdateShiftRecordStatusResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateShiftRecordStatusCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await shiftRecordCommandService.Handle(command, cancellationToken);

        return ShiftCoordinationActionResultAssembler.ToActionResultFromUpdateShiftRecordStatusResult(
            this,
            result,
            problemDetailsFactory,
            updatedShiftRecord => Ok(
                ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity(updatedShiftRecord)
            )
        );
    }
}