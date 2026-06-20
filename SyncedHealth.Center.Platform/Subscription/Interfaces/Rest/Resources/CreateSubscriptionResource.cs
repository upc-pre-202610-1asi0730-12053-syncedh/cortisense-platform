namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Payload for creating a new subscription.
/// </summary>
public record CreateSubscriptionResource
{
    /// <summary>The ID of the organization purchasing the subscription.</summary>
    /// <example>10</example>
    public int OrganizationId { get; init; }

    /// <summary>The ID of the selected plan.</summary>
    /// <example>2</example>
    public int PlanId { get; init; }
}
