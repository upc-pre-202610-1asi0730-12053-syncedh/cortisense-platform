namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create subscription resource in the CortiSense Platform.
/// </summary>
public record CreateSubscriptionResource
{
    public int OrganizationId { get; init; }

    public int PlanId { get; init; }

    public DateTimeOffset? StartedAt { get; init; }
}