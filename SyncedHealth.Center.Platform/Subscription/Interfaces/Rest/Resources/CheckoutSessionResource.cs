namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents a payment or checkout session attempt.
/// </summary>
public record CheckoutSessionResource
{
    public int Id { get; init; }

    public int OrganizationId { get; init; }

    public int AdministratorId { get; init; }

    public int SubscriptionId { get; init; }

    public int PlanId { get; init; }

    public string PlanCode { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string? StripeSessionId { get; init; }

    public string? StripeUrl { get; init; }

    public string? StripeSubscriptionId { get; init; }

    public string? StripeCustomerId { get; init; }

    public DateTimeOffset? CreatedAt { get; init; }

    public DateTimeOffset? CompletedAt { get; init; }

    public DateTimeOffset? FailedAt { get; init; }

    public DateTimeOffset? CancelledAt { get; init; }

    public string? ErrorMessage { get; init; }
}