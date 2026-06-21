namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Errors;

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