using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class ShiftCoordinationActionResultAssembler
{
    private static int ToStatusCodeFromShiftCoordinationError(ShiftCoordinationError error)
    {
        return error switch
        {
            ShiftCoordinationError.ShiftRecordNotFound => StatusCodes.Status404NotFound,
            ShiftCoordinationError.InvalidShiftRecordData => StatusCodes.Status400BadRequest,
            ShiftCoordinationError.InvalidShiftType => StatusCodes.Status400BadRequest,
            ShiftCoordinationError.InvalidShiftStatus => StatusCodes.Status400BadRequest,
            ShiftCoordinationError.InvalidShiftSchedule => StatusCodes.Status400BadRequest,
            ShiftCoordinationError.OperationCancelled => StatusCodes.Status409Conflict,
            ShiftCoordinationError.DatabaseError => StatusCodes.Status500InternalServerError,
            ShiftCoordinationError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
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