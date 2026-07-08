namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to create care team.
/// </summary>
public record CreateCareTeamCommand(
    int OrganizationId,
    string Name,
    int WorkAreaId,
    int SupervisorId,
    string Status
);