namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

/// <summary>
/// Represents the checkout session status response in the CortiSense Platform.
/// </summary>
public record CheckoutSessionStatusResponse(
    string StripeSessionId,
    string Status,
    string PaymentStatus,
    bool Activated
);