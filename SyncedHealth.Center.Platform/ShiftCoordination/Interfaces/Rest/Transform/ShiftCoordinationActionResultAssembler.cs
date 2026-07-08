using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the shift coordination action result assembler in the CortiSense Platform.
/// </summary>
public static class ShiftCoordinationActionResultAssembler
{
    private static int ToStatusCodeFromShiftCoordinationError(ShiftCoordinationError error)
    {
        return error switch
        {
            ShiftCoordinationError.ShiftRecordNotFound => StatusCodes.Status404NotFound,
            ShiftCoordinationError.WorkAreaNotFound => StatusCodes.Status404NotFound,
            ShiftCoordinationError.SpecialtyNotFound => StatusCodes.Status404NotFound,
            ShiftCoordinationError.CareTeamNotFound => StatusCodes.Status404NotFound,
            ShiftCoordinationError.TeamMemberNotFound => StatusCodes.Status404NotFound,

            ShiftCoordinationError.InvalidShiftRecordData => StatusCodes.Status400BadRequest,
            ShiftCoordinationError.InvalidShiftType => StatusCodes.Status400BadRequest,
            ShiftCoordinationError.InvalidShiftStatus => StatusCodes.Status400BadRequest,
            ShiftCoordinationError.InvalidShiftSchedule => StatusCodes.Status400BadRequest,

            ShiftCoordinationError.UserAlreadyAssignedToTeam => StatusCodes.Status409Conflict,
            ShiftCoordinationError.SupervisorAlreadyAssignedToActiveTeam => StatusCodes.Status409Conflict,
            ShiftCoordinationError.InactiveCareTeam => StatusCodes.Status409Conflict,
            ShiftCoordinationError.OperationCancelled => StatusCodes.Status409Conflict,

            ShiftCoordinationError.DatabaseError => StatusCodes.Status500InternalServerError,
            ShiftCoordinationError.InternalServerError => StatusCodes.Status500InternalServerError,

            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        var error = result.Error is ShiftCoordinationError shiftCoordinationError
            ? shiftCoordinationError
            : ShiftCoordinationError.InternalServerError;

        var statusCode = ToStatusCodeFromShiftCoordinationError(error);

        return new ObjectResult(new
        {
            error = result.Error?.ToString(),
            message = result.Message
        })
        {
            StatusCode = statusCode
        };
    }

    public static IActionResult ToActionResultFromCreateShiftRecordResult(
        ControllerBase controller,
        Result<ShiftRecord> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ShiftRecord, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromShiftCoordinationError(
            (ShiftCoordinationError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }

    public static IActionResult ToActionResultFromUpdateShiftRecordStatusResult(
        ControllerBase controller,
        Result<ShiftRecord> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ShiftRecord, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromShiftCoordinationError(
            (ShiftCoordinationError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }
}