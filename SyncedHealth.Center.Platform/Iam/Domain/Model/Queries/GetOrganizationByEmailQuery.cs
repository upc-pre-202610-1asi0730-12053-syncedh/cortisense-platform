namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/// <summary>
/// Represents a query to get organization by email in the CortiSense Platform.
/// </summary>
public record GetOrganizationByEmailQuery(string Email);