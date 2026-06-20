namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;

public enum ShiftCoordinationError
{
    None,
    ShiftRecordNotFound,
    InvalidShiftRecordData,
    InvalidShiftType,
    InvalidShiftStatus,
    InvalidShiftSchedule,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}