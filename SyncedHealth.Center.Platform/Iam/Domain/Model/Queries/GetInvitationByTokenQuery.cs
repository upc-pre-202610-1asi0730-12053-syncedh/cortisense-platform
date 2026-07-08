namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/// <summary>
/// Represents a query to get invitation by token in the CortiSense Platform.
/// </summary>
public record GetInvitationByTokenQuery(string Token);