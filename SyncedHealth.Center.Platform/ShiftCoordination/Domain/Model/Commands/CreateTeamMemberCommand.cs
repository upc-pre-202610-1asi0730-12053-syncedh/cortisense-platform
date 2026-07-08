namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to create team member.
/// </summary>
public record CreateTeamMemberCommand(
    int TeamId,
    int UserId
);