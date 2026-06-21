namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Payload for initiating a new checkout session.
/// </summary>
public record CreateCheckoutSessionResource
{
    /// <summary>The organization initiating the checkout.</summary>
    /// <example>10</example>
    public int OrganizationId { get; init; }

    /// <summary>The user ID of the administrator initiating the checkout.</summary>
    /// <example>100</example>
    public int AdministratorId { get; init; }

    /// <summary>The ID of the active or pending subscription.</summary>
    /// <example>1</example>
    public int SubscriptionId { get; init; }

    /// <summary>The ID of the plan to purchase.</summary>
    /// <example>2</example>
    public int PlanId { get; init; }

    /// <summary>The code of the plan to purchase.</summary>
    /// <example>premium_annual</example>
    public string PlanCode { get; init; } = string.Empty;
}
