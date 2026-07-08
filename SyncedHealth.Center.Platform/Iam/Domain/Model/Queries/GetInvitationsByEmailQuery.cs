namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/// <summary>
/// Represents a query to get invitations by email in the CortiSense Platform.
/// </summary>
public record GetInvitationsByEmailQuery(string Email);