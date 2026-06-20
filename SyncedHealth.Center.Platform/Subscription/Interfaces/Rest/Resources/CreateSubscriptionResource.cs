namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Payload for creating a new subscription.
/// </summary>
public record CreateSubscriptionResource
{
    public int OrganizationId { get; init; }

    public int PlanId { get; init; }

    public DateTimeOffset? StartedAt { get; init; }
}