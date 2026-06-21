using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class DeleteTeamMemberCommandFromRouteAssembler
{
    public static DeleteTeamMemberCommand ToCommandFromRoute(int id)
    {
        return new DeleteTeamMemberCommand(id);
    }
}