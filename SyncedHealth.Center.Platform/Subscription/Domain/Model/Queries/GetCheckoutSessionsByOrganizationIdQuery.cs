namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;

/// <summary>
/// Represents a query to get checkout sessions by organization id in the CortiSense Platform.
/// </summary>
public record GetCheckoutSessionsByOrganizationIdQuery(int OrganizationId);
