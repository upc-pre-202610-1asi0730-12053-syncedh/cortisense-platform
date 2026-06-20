namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

public record CreateSubscriptionCommand(
    int OrganizationId,
    int PlanId,
    DateTimeOffset? StartedAt
);