using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest;

[ApiController]
[Route("api/v1/shiftRecords")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Shift Record Endpoints.")]
/// <summary>
/// Controller for shift records REST API endpoints.
/// </summary>
public class ShiftRecordsController(
    IShiftRecordCommandService shiftRecordCommandService,
    IShiftRecordQueryService shiftRecordQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet("suggestions")]
    [SwaggerOperation("Get Replacement Suggestions", "Get available staff in a work area who can cover a shift.")]
    public async Task<IActionResult> GetReplacementSuggestions(
        [FromQuery] int workAreaId,
        [FromQuery] int organizationId,
        CancellationToken cancellationToken)
    {
        if (workAreaId <= 0 || organizationId <= 0)
            return BadRequest(new { Message = "workAreaId and organizationId are required." });

        var shiftRecords = await shiftRecordQueryService.Handle(
            new GetReplacementSuggestionsQuery(workAreaId, organizationId),
            cancellationToken);

        return Ok(shiftRecords.Select(ShiftRecordResourceFromEntityAssembler.ToResourceFromEntity));
    }

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

    [HttpPost("{id:int}/check-in")]
    [SwaggerOperation("Check In Shift", "Register check-in time for a shift record.")]
    public async Task<IActionResult> CheckInShiftRecord(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new CheckInShiftRecordCommand(id, DateTimeOffset.UtcNow);
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

    [HttpPost("{id:int}/check-out")]
    [SwaggerOperation("Check Out Shift", "Register check-out time for a shift record.")]
    public async Task<IActionResult> CheckOutShiftRecord(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new CheckOutShiftRecordCommand(id, DateTimeOffset.UtcNow);
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

    [HttpPost("{id:int}/evaluate-risk")]
    [SwaggerOperation("Evaluate Critical Shift", "Evaluate if a scheduled shift represents a danger due to fatigue (US-20).")]
    public async Task<IActionResult> EvaluateCriticalShift(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new EvaluateCriticalShiftCommand(id);
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

    [HttpPost("{id:int}/block")]
    [SwaggerOperation("Block Shift", "Block a shift preventively (US-21).")]
    public async Task<IActionResult> BlockShift(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new BlockShiftCommand(id);
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

    [HttpPost("{id:int}/reassign")]
    [SwaggerOperation("Reassign Shift", "Reassign a blocked shift to a suggested replacement (US-23).")]
    public async Task<IActionResult> ReassignShift(
        int id,
        [FromBody] ReassignShiftResource resource,
        CancellationToken cancellationToken)
    {
        var command = new ReassignShiftCommand(id, resource.NewUserId);
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