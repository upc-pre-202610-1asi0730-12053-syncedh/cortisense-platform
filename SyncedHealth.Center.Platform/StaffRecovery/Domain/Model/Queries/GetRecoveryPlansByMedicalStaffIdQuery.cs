namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;

/// <summary>
/// Represents a query to get recovery plans by medical staff id in the CortiSense Platform.
/// </summary>
public record GetRecoveryPlansByMedicalStaffIdQuery(int MedicalStaffId);