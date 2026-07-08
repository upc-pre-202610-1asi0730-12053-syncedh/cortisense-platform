using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Errors;

namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Transform;

/// <summary>
/// Represents the staff recovery action result assembler in the CortiSense Platform.
/// </summary>
public static class StaffRecoveryActionResultAssembler
{
    private static int ToStatusCodeFromStaffRecoveryError(StaffRecoveryError error)
    {
        return error switch
        {
            StaffRecoveryError.RecoveryPlanNotFound => StatusCodes.Status404NotFound,

            StaffRecoveryError.InvalidRecoveryPlanData => StatusCodes.Status400BadRequest,
            StaffRecoveryError.InvalidRecoveryPlanStatus => StatusCodes.Status400BadRequest,
            StaffRecoveryError.InvalidSuggestedRestDays => StatusCodes.Status400BadRequest,

            StaffRecoveryError.OperationCancelled => StatusCodes.Status409Conflict,

            StaffRecoveryError.DatabaseError => StatusCodes.Status500InternalServerError,
            StaffRecoveryError.InternalServerError => StatusCodes.Status500InternalServerError,

            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        var error = result.Error is StaffRecoveryError staffRecoveryError
            ? staffRecoveryError
            : StaffRecoveryError.InternalServerError;

        var statusCode = ToStatusCodeFromStaffRecoveryError(error);

        return new ObjectResult(new
        {
            error = result.Error?.ToString(),
            message = result.Message
        })
        {
            StatusCode = statusCode
        };
    }
}