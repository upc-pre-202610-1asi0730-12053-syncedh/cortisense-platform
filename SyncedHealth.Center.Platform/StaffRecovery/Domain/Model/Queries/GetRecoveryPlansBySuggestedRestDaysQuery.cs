namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;

/// <summary>
/// Represents a query to get recovery plans by suggested rest days in the CortiSense Platform.
/// </summary>
public record GetRecoveryPlansBySuggestedRestDaysQuery(int SuggestedRestDays);