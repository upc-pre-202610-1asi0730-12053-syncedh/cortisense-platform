namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents the update subscription resource in the CortiSense Platform.
/// </summary>
public record UpdateSubscriptionResource
{
    public int PlanId { get; init; }
}
