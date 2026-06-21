namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

public record CreateTeamMemberCommand(
    int TeamId,
    int UserId
);