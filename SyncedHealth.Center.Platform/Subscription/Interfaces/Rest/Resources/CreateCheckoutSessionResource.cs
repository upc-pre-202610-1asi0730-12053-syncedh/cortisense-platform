namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create checkout session resource in the CortiSense Platform.
/// </summary>
public record CreateCheckoutSessionResource
{
    public int OrganizationId { get; init; }

    public int AdministratorId { get; init; }

    public int SubscriptionId { get; init; }

    public int PlanId { get; init; }

    public string PlanCode { get; init; } = string.Empty;
}
