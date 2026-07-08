namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/// <summary>
/// Represents a query to get user by email in the CortiSense Platform.
/// </summary>
public record GetUserByEmailQuery(string Email);