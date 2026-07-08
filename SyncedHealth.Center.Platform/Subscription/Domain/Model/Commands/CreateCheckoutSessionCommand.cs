namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

/// <summary>
/// Command to create checkout session.
/// </summary>
public record CreateCheckoutSessionCommand(
    int OrganizationId,
    int AdministratorId,
    int SubscriptionId,
    int PlanId,
    string PlanCode
);