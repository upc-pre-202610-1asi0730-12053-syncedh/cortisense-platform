using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Application.OutboundServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;
using SyncedHealth.Center.Platform.Subscription.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest;

[ApiController]
[Route("api/v1/billing")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Billing Endpoints.")]
public class BillingController(
    IStripeBillingService stripeBillingService,
    ICheckoutSessionRepository checkoutSessionRepository,
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<SubscriptionMessages> localizer) : ControllerBase
{
    private readonly IStringLocalizer<SubscriptionMessages> _localizer = localizer;

    [HttpPost("create-checkout-session")]
    [SwaggerOperation(
        "Create Stripe Checkout Session",
        "Create a Stripe checkout session for an existing subscription.")]
    public async Task<IActionResult> CreateCheckoutSession(
        [FromBody] CreateStripeCheckoutSessionResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await stripeBillingService.CreateCheckoutSessionAsync(
                resource,
                cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpGet("checkout-session-status")]
    [SwaggerOperation(
        "Get Stripe Checkout Session Status",
        "Get Stripe checkout session status by session_id.")]
    public async Task<IActionResult> GetCheckoutSessionStatus(
        [FromQuery(Name = "session_id")] string sessionId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            return BadRequest(new { message = _localizer["SessionIdRequired"].Value });

        try
        {
            var response = await stripeBillingService.GetCheckoutSessionStatusAsync(
                sessionId,
                cancellationToken);

            return Ok(response);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpPost("cancel-checkout-session")]
    [SwaggerOperation(
        "Cancel Checkout Session",
        "Cancel a local checkout session and its subscription.")]
    public async Task<IActionResult> CancelCheckoutSession(
        [FromBody] CancelCheckoutSessionResource resource,
        CancellationToken cancellationToken)
    {
        var checkoutSession = await checkoutSessionRepository.FindByIdAsync(
            resource.CheckoutSessionId,
            cancellationToken);

        if (checkoutSession is null)
            return NotFound(new { message = _localizer["CheckoutSessionNotFound"].Value });

        if (checkoutSession.Status == ECheckoutSessionStatus.Completed)
            return Conflict(new { message = _localizer["CheckoutSessionAlreadyCompleted"].Value });

        checkoutSession.MarkAsCancelled();
        checkoutSessionRepository.Update(checkoutSession);

        var subscription = await subscriptionRepository.FindByIdAsync(
            checkoutSession.SubscriptionId,
            cancellationToken);

        if (subscription is not null)
        {
            subscription.Cancel();
            subscriptionRepository.Update(subscription);
        }

        await unitOfWork.CompleteAsync(cancellationToken);

        return Ok(new CancelCheckoutSessionResponse(
            true,
            checkoutSession.Id));
    }

    [HttpPost("webhook")]
    [SwaggerOperation(
        "Stripe Webhook",
        "Receives Stripe webhook events.")]
    public IActionResult HandleWebhook()
    {
        return Ok(new { received = true });
    }
}