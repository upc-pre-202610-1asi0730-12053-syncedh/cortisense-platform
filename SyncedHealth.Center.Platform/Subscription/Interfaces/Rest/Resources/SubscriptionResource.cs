namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents an organization's active subscription.
/// </summary>
public record SubscriptionResource
{
    /// <summary>The unique identifier of the subscription.</summary>
    /// <example>1</example>
    public int Id { get; init; }

    /// <summary>The identifier of the organization that owns the subscription.</summary>
    /// <example>10</example>
    public int OrganizationId { get; init; }

    /// <summary>The identifier of the active plan.</summary>
    /// <example>2</example>
    public int PlanId { get; init; }

    /// <summary>The current status of the subscription.</summary>
    /// <example>Active</example>
    public string Status { get; init; } = string.Empty;

    /// <summary>The date and time when the subscription started.</summary>
    /// <example>2023-10-01T12:00:00Z</example>
    public DateTimeOffset StartedAt { get; init; }
}
