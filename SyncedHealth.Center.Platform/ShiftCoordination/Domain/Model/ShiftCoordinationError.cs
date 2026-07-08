namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;

/// <summary>
/// Represents the shift coordination error in the CortiSense Platform.
/// </summary>
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