namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents a payment or checkout session attempt.
/// </summary>
public record CheckoutSessionResource
{
    /// <summary>The unique identifier of the checkout session.</summary>
    /// <example>50</example>
    public int Id { get; init; }

    /// <summary>The organization initiating the checkout.</summary>
    /// <example>10</example>
    public int OrganizationId { get; init; }

    /// <summary>The user ID of the administrator initiating the checkout.</summary>
    /// <example>100</example>
    public int AdministratorId { get; init; }

    /// <summary>The ID of the subscription being paid for (if applicable).</summary>
    /// <example>1</example>
    public int SubscriptionId { get; init; }

    /// <summary>The ID of the plan being purchased.</summary>
    /// <example>2</example>
    public int PlanId { get; init; }

    /// <summary>The internal code of the plan being purchased.</summary>
    /// <example>premium_annual</example>
    public string PlanCode { get; init; } = string.Empty;

    /// <summary>The current status of the checkout session.</summary>
    /// <example>Pending</example>
    public string Status { get; init; } = string.Empty;
}
