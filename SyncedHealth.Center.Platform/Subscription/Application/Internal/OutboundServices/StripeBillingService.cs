using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Application.OutboundServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Infrastructure.Stripe.Configuration;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.OutboundServices;

public class StripeBillingService(
    IPlanRepository planRepository,
    ICheckoutSessionRepository checkoutSessionRepository,
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork,
    IOptions<StripeSettings> stripeSettingsOptions)
    : IStripeBillingService
{
    private readonly StripeSettings _stripeSettings = stripeSettingsOptions.Value;

    public async Task<CreateStripeCheckoutSessionResponse> CreateCheckoutSessionAsync(
        CreateStripeCheckoutSessionResource resource,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_stripeSettings.SecretKey))
            throw new InvalidOperationException("Stripe:SecretKey is not configured.");

        if (string.IsNullOrWhiteSpace(resource.CustomerEmail))
            throw new InvalidOperationException("CustomerEmail is required.");

        var plan = await planRepository.FindByIdAsync(resource.PlanId, cancellationToken);

        if (plan is null)
            throw new InvalidOperationException("Plan not found.");

        var subscription = await subscriptionRepository.FindByIdAsync(
            resource.SubscriptionId,
            cancellationToken);

        if (subscription is null)
            throw new InvalidOperationException("Subscription not found.");

        var checkoutSession = new Domain.Model.Aggregates.CheckoutSession(
            new CreateCheckoutSessionCommand(
                resource.OrganizationId,
                resource.AdministratorId,
                resource.SubscriptionId,
                resource.PlanId,
                resource.PlanCode
            )
        );

        await checkoutSessionRepository.AddAsync(checkoutSession, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

        var metadata = new Dictionary<string, string>
        {
            ["organizationId"] = resource.OrganizationId.ToString(),
            ["administratorId"] = resource.AdministratorId.ToString(),
            ["subscriptionId"] = resource.SubscriptionId.ToString(),
            ["checkoutSessionId"] = checkoutSession.Id.ToString(),
            ["planId"] = resource.PlanId.ToString(),
            ["planCode"] = resource.PlanCode
        };

        var cancelUrl = _stripeSettings.CancelUrl
            .Replace("{CHECKOUT_SESSION_ID}", checkoutSession.Id.ToString())
            .Replace("{PLAN_CODE}", resource.PlanCode);

        var unitAmount = Convert.ToInt64(
            decimal.Round(plan.Price * 100, 0, MidpointRounding.AwayFromZero));

        var interval = plan.BillingPeriod == EBillingPeriod.Yearly
            ? "year"
            : "month";

        var options = new SessionCreateOptions
        {
            Mode = "subscription",
            CustomerEmail = resource.CustomerEmail,
            ClientReferenceId = checkoutSession.Id.ToString(),
            SuccessUrl = _stripeSettings.SuccessUrl,
            CancelUrl = cancelUrl,
            PaymentMethodTypes = ["card"],
            Metadata = metadata,
            SubscriptionData = new SessionSubscriptionDataOptions
            {
                Metadata = metadata
            },
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = plan.Currency.ToLowerInvariant(),
                        UnitAmount = unitAmount,
                        Recurring = new SessionLineItemPriceDataRecurringOptions
                        {
                            Interval = interval
                        },
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"CortiSense {plan.Name}"
                        }
                    }
                }
            ]
        };

        var sessionService = new SessionService();
        var stripeSession = await sessionService.CreateAsync(
            options,
            cancellationToken: cancellationToken);

        checkoutSession.AttachStripeSession(stripeSession.Id, stripeSession.Url);
        checkoutSessionRepository.Update(checkoutSession);
        await unitOfWork.CompleteAsync(cancellationToken);

        return new CreateStripeCheckoutSessionResponse(
            stripeSession.Url,
            stripeSession.Id,
            checkoutSession.OrganizationId,
            checkoutSession.AdministratorId,
            checkoutSession.SubscriptionId,
            checkoutSession.Id
        );
    }

    public async Task<CheckoutSessionStatusResponse> GetCheckoutSessionStatusAsync(
        string stripeSessionId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_stripeSettings.SecretKey))
            throw new InvalidOperationException("Stripe:SecretKey is not configured.");

        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(
            stripeSessionId,
            cancellationToken: cancellationToken);

        var isPaid = session.Status == "complete" && session.PaymentStatus == "paid";
        var activated = false;

        if (isPaid)
        {
            var checkoutSession = await checkoutSessionRepository.FindByStripeSessionIdAsync(
                session.Id,
                cancellationToken);

            if (checkoutSession is not null)
            {
                checkoutSession.MarkAsCompleted(
                    session.SubscriptionId,
                    session.CustomerId);

                checkoutSessionRepository.Update(checkoutSession);

                var subscription = await subscriptionRepository.FindByIdAsync(
                    checkoutSession.SubscriptionId,
                    cancellationToken);

                if (subscription is not null)
                {
                    subscription.Activate(
                        session.SubscriptionId,
                        session.CustomerId);

                    subscriptionRepository.Update(subscription);
                    activated = true;
                }

                await unitOfWork.CompleteAsync(cancellationToken);
            }
        }

        return new CheckoutSessionStatusResponse(
            session.Id,
            session.Status ?? string.Empty,
            session.PaymentStatus ?? string.Empty,
            activated
        );
    }
}