namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;

public enum ShiftCoordinationError
{
    None,

    // Existing shift record errors
    ShiftRecordNotFound,
    InvalidShiftRecord,
    InvalidShiftRecordData,
    InvalidShiftType,
    InvalidShiftStatus,
    InvalidShiftSchedule,

    // Team management errors
    WorkAreaNotFound,
    SpecialtyNotFound,
    CareTeamNotFound,
    TeamMemberNotFound,
    UserAlreadyAssignedToTeam,
    SupervisorAlreadyAssignedToActiveTeam,
    InactiveCareTeam,

    // Common errors
    OperationCancelled,
    DatabaseError,
    InternalServerError
}