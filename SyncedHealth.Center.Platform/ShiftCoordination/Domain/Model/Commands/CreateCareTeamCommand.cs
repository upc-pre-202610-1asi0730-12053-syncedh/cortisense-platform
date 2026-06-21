namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

public record CreateCareTeamCommand(
    int OrganizationId,
    string Name,
    int WorkAreaId,
    int SupervisorId,
    string Status
);