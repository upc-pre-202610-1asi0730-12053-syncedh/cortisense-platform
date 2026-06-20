namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

public record CheckoutSessionStatusResponse(
    string StripeSessionId,
    string Status,
    string PaymentStatus,
    bool Activated
);