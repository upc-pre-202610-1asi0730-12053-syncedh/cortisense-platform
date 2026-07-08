namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to update care team.
/// </summary>
public record UpdateCareTeamCommand(
    int Id,
    string Name,
    int WorkAreaId,
    int SupervisorId,
    string Status
);