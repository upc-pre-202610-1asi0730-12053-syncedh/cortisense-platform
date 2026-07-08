namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;

/// <summary>
/// Represents a query to get recovery plans by status in the CortiSense Platform.
/// </summary>
public record GetRecoveryPlansByStatusQuery(string Status);