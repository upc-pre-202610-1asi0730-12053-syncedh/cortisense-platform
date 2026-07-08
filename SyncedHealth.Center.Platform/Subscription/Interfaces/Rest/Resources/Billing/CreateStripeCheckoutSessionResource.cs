namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

/// <summary>
/// Represents the create stripe checkout session resource in the CortiSense Platform.
/// </summary>
public record CreateStripeCheckoutSessionResource(
    int OrganizationId,
    int AdministratorId,
    int SubscriptionId,
    int PlanId,
    string PlanCode,
    string CustomerEmail
);