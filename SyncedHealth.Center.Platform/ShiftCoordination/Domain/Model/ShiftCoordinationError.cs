namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;

public enum ShiftCoordinationError
{
    None,
    ShiftRecordNotFound,
    InvalidShiftRecord,

    WorkAreaNotFound,
    SpecialtyNotFound,
    CareTeamNotFound,
    TeamMemberNotFound,

    UserAlreadyAssignedToTeam,
    SupervisorAlreadyAssignedToActiveTeam,
    InactiveCareTeam,

    OperationCancelled,
    DatabaseError,
    InternalServerError
}