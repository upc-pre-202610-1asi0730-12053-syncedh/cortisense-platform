namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

/// <summary>
/// Represents the cancel checkout session resource in the CortiSense Platform.
/// </summary>
public record CancelCheckoutSessionResource(
    int CheckoutSessionId
);