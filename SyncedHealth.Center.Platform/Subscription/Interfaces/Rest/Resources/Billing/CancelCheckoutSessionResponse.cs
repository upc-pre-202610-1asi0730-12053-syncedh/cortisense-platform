namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

/// <summary>
/// Represents the cancel checkout session response in the CortiSense Platform.
/// </summary>
public record CancelCheckoutSessionResponse(
    bool Cancelled,
    int CheckoutSessionId
);