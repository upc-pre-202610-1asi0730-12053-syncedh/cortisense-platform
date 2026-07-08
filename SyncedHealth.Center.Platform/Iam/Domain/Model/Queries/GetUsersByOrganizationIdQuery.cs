namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/// <summary>
/// Represents a query to get users by organization id in the CortiSense Platform.
/// </summary>
public record GetUsersByOrganizationIdQuery(int OrganizationId);