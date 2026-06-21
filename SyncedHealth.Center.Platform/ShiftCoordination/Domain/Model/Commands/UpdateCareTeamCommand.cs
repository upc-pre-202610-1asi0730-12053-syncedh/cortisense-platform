namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

public record UpdateCareTeamCommand(
    int Id,
    string Name,
    int WorkAreaId,
    int SupervisorId,
    string Status
);