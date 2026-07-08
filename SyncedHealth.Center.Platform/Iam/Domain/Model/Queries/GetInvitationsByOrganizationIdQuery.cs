namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/// <summary>
/// Represents a query to get invitations by organization id in the CortiSense Platform.
/// </summary>
public record GetInvitationsByOrganizationIdQuery(int OrganizationId);