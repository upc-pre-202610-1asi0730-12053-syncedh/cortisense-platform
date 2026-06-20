using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Interfaces.REST.ProblemDetails;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

public static class ClinicalRiskAssessmentActionResultAssembler
{
    private static int ToStatusCodeFromClinicalRiskAssessmentError(ClinicalRiskAssessmentError error)
    {
        return error switch
        {
            ClinicalRiskAssessmentError.RiskAssessmentNotFound => StatusCodes.Status404NotFound,
            ClinicalRiskAssessmentError.ClinicalAlertNotFound => StatusCodes.Status404NotFound,
            ClinicalRiskAssessmentError.VitalSignAnomalyNotFound => StatusCodes.Status404NotFound,
            ClinicalRiskAssessmentError.VitalSignReadingNotFound => StatusCodes.Status404NotFound,

            ClinicalRiskAssessmentError.InvalidRiskLevel => StatusCodes.Status400BadRequest,
            ClinicalRiskAssessmentError.InvalidSeverity => StatusCodes.Status400BadRequest,
            ClinicalRiskAssessmentError.InvalidStatus => StatusCodes.Status400BadRequest,
            ClinicalRiskAssessmentError.InvalidBiometricData => StatusCodes.Status400BadRequest,

            ClinicalRiskAssessmentError.OperationCancelled => StatusCodes.Status409Conflict,

            ClinicalRiskAssessmentError.DatabaseError => StatusCodes.Status500InternalServerError,
            ClinicalRiskAssessmentError.InternalServerError => StatusCodes.Status500InternalServerError,

            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateRiskAssessmentResult(
        ControllerBase controller,
        Result<RiskAssessment> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<RiskAssessment, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromClinicalRiskAssessmentError(
            (ClinicalRiskAssessmentError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }

    public static IActionResult ToActionResultFromCreateClinicalAlertResult(
        ControllerBase controller,
        Result<ClinicalAlert> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ClinicalAlert, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromClinicalRiskAssessmentError(
            (ClinicalRiskAssessmentError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }

    public static IActionResult ToActionResultFromUpdateClinicalAlertStatusResult(
        ControllerBase controller,
        Result<ClinicalAlert> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ClinicalAlert, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromClinicalRiskAssessmentError(
            (ClinicalRiskAssessmentError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }

    public static IActionResult ToActionResultFromCreateVitalSignAnomalyResult(
        ControllerBase controller,
        Result<VitalSignAnomaly> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<VitalSignAnomaly, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromClinicalRiskAssessmentError(
            (ClinicalRiskAssessmentError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }

    public static IActionResult ToActionResultFromUpdateVitalSignAnomalyStatusResult(
        ControllerBase controller,
        Result<VitalSignAnomaly> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<VitalSignAnomaly, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromClinicalRiskAssessmentError(
            (ClinicalRiskAssessmentError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }

    public static IActionResult ToActionResultFromCreateVitalSignReadingResult(
        ControllerBase controller,
        Result<VitalSignReading> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<VitalSignReading, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromClinicalRiskAssessmentError(
            (ClinicalRiskAssessmentError)result.Error!
        );

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message
        );
    }
}