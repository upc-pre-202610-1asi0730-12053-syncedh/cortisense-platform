using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.Shared.Resources.Errors;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

public static class SubscriptionActionResultAssembler
{
    private static int ToStatusCodeFromSubscriptionError(SubscriptionError error)
    {
        return error switch
        {
            SubscriptionError.PlanNotFound => StatusCodes.Status404NotFound,
            SubscriptionError.SubscriptionNotFound => StatusCodes.Status404NotFound,
            SubscriptionError.CheckoutSessionNotFound => StatusCodes.Status404NotFound,
            SubscriptionError.InvalidSubscriptionState => StatusCodes.Status400BadRequest,
            SubscriptionError.OperationCancelled => StatusCodes.Status409Conflict,
            SubscriptionError.DatabaseError => StatusCodes.Status500InternalServerError,
            SubscriptionError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateSubscriptionResult(
        ControllerBase controller,
        Result<Domain.Model.Aggregates.Subscription> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Domain.Model.Aggregates.Subscription, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromSubscriptionError((SubscriptionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromUpdateSubscriptionResult(
        ControllerBase controller,
        Result<Domain.Model.Aggregates.Subscription> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Domain.Model.Aggregates.Subscription, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromSubscriptionError((SubscriptionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromCreateCheckoutSessionResult(
        ControllerBase controller,
        Result<CheckoutSession> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<CheckoutSession, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromSubscriptionError((SubscriptionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
