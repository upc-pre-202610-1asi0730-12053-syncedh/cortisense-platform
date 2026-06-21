using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Application.OutboundServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Infrastructure.Stripe.Configuration;
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
    IStringLocalizer<SubscriptionMessages> localizer,
    IOptions<StripeSettings> stripeOptions) : ControllerBase
{
    private readonly IStringLocalizer<SubscriptionMessages> _localizer = localizer;
    private readonly StripeSettings _stripeSettings = stripeOptions.Value;

    [AllowAnonymous]
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

    [AllowAnonymous]
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

    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpPost("webhook")]
    [SwaggerOperation(
        "Stripe Webhook",
        "Receives Stripe webhook events.")]
    public async Task<IActionResult> HandleWebhook(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_stripeSettings.WebhookSecret))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "STRIPE_WEBHOOK_SECRET is not configured."
            });
        }

        var signature = Request.Headers["Stripe-Signature"].ToString();

        if (string.IsNullOrWhiteSpace(signature))
            return BadRequest(new { message = "Missing Stripe-Signature header." });

        string json;

        using (var reader = new StreamReader(HttpContext.Request.Body))
        {
            json = await reader.ReadToEndAsync(cancellationToken);
        }

        Stripe.Event stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                signature,
                _stripeSettings.WebhookSecret);
        }
        catch (StripeException exception)
        {
            return BadRequest(new { message = $"Webhook Error: {exception.Message}" });
        }

        try
        {
            if (stripeEvent.Type == "checkout.session.completed")
            {
                if (stripeEvent.Data.Object is Session session &&
                    session.PaymentStatus == "paid")
                {
                    await stripeBillingService.GetCheckoutSessionStatusAsync(
                        session.Id,
                        cancellationToken);
                }
            }

            if (stripeEvent.Type == "checkout.session.expired")
            {
                if (stripeEvent.Data.Object is Session session)
                {
                    var checkoutSession = await checkoutSessionRepository.FindByStripeSessionIdAsync(
                        session.Id,
                        cancellationToken);

                    if (checkoutSession is not null &&
                        checkoutSession.Status != ECheckoutSessionStatus.Completed)
                    {
                        checkoutSession.MarkAsFailed("Stripe checkout session expired.");
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
                    }
                }
            }

            return Ok(new
            {
                received = true,
                type = stripeEvent.Type
            });
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = exception.Message
            });
        }
    }
}