namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

/// <summary>
/// Command to create subscription.
/// </summary>
public record CreateSubscriptionCommand(
    int OrganizationId,
    int PlanId,
    DateTimeOffset? StartedAt
);