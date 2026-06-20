namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

public record CreateStripeCheckoutSessionResponse(
    string CheckoutUrl,
    string StripeSessionId,
    int OrganizationId,
    int AdministratorId,
    int SubscriptionId,
    int CheckoutSessionId
);