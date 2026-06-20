namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Stripe.Configuration;

public class StripeSettings
{
    public string SecretKey { get; set; } = string.Empty;

    public string WebhookSecret { get; set; } = string.Empty;

    public string SuccessUrl { get; set; } =
        "http://localhost:5173/checkout/success?session_id={CHECKOUT_SESSION_ID}";

    public string CancelUrl { get; set; } =
        "http://localhost:5173/checkout/cancelled?checkoutSessionId={CHECKOUT_SESSION_ID}";
}