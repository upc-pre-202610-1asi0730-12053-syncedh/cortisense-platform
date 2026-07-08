namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/// <summary>
/// Represents a query to get user by username in the CortiSense Platform.
/// </summary>
public record GetUserByUsernameQuery(string Username);