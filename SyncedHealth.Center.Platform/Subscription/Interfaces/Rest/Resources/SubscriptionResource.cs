namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents the subscription resource in the CortiSense Platform.
/// </summary>
public record SubscriptionResource
{
    public int Id { get; init; }

    public int OrganizationId { get; init; }

    public int PlanId { get; init; }

    public string Status { get; init; } = string.Empty;

    public DateTimeOffset StartedAt { get; init; }

    public DateTimeOffset? CancelledAt { get; init; }

    public string? StripeSubscriptionId { get; init; }

    public string? StripeCustomerId { get; init; }
}