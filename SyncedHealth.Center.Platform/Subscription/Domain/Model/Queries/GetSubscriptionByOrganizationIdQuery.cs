namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;

/// <summary>
/// Represents a query to get subscription by organization id in the CortiSense Platform.
/// </summary>
public record GetSubscriptionByOrganizationIdQuery(int OrganizationId);
