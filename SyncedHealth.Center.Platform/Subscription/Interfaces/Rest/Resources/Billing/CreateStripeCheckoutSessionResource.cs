namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources.Billing;

public record CreateStripeCheckoutSessionResource(
    int OrganizationId,
    int AdministratorId,
    int SubscriptionId,
    int PlanId,
    string PlanCode,
    string CustomerEmail
);