namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Errors;

/// <summary>
/// Represents the staff recovery error in the CortiSense Platform.
/// </summary>
public enum StaffRecoveryError
{
    None,

    RecoveryPlanNotFound,
    InvalidRecoveryPlanData,
    InvalidRecoveryPlanStatus,
    InvalidSuggestedRestDays,

    OperationCancelled,
    DatabaseError,
    InternalServerError
}