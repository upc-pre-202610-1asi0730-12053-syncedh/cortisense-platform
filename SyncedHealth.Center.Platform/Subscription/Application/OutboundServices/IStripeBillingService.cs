using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

namespace SyncedHealth.Center.Platform.Subscription.Application.OutboundServices;

public interface IStripeBillingService
{
    Task<CreateStripeCheckoutSessionResponse> CreateCheckoutSessionAsync(
        CreateStripeCheckoutSessionResource resource,
        CancellationToken cancellationToken);

    Task<CheckoutSessionStatusResponse> GetCheckoutSessionStatusAsync(
        string stripeSessionId,
        CancellationToken cancellationToken);
}