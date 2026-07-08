using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

namespace SyncedHealth.Center.Platform.Subscription.Application.OutboundServices;

/// <summary>
/// Represents the stripe billing service in the CortiSense Platform.
/// </summary>
public interface IStripeBillingService
{
    Task<CreateStripeCheckoutSessionResponse> CreateCheckoutSessionAsync(
        CreateStripeCheckoutSessionResource resource,
        CancellationToken cancellationToken);

    Task<CheckoutSessionStatusResponse> GetCheckoutSessionStatusAsync(
        string stripeSessionId,
        CancellationToken cancellationToken);
}