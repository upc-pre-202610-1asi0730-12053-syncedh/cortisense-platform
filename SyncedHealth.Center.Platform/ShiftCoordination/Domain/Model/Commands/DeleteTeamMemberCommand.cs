namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to delete team member.
/// </summary>
public record DeleteTeamMemberCommand(
    int Id
);