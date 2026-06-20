namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Payload for upgrading or downgrading an existing subscription.
/// </summary>
public record UpdateSubscriptionResource
{
    /// <summary>The ID of the new plan to switch to.</summary>
    /// <example>3</example>
    public int PlanId { get; init; }
}
