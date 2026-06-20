namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

public record CancelCheckoutSessionResponse(
    bool Cancelled,
    int CheckoutSessionId
);