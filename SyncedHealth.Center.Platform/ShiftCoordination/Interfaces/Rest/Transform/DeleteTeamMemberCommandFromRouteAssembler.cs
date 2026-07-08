using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the delete team member command from route assembler in the CortiSense Platform.
/// </summary>
public static class DeleteTeamMemberCommandFromRouteAssembler
{
    public static DeleteTeamMemberCommand ToCommandFromRoute(int id)
    {
        return new DeleteTeamMemberCommand(id);
    }
}