namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

/// <summary>
/// Represents the create stripe checkout session response in the CortiSense Platform.
/// </summary>
public record CreateStripeCheckoutSessionResponse(
    string CheckoutUrl,
    string StripeSessionId,
    int OrganizationId,
    int AdministratorId,
    int SubscriptionId,
    int CheckoutSessionId
);