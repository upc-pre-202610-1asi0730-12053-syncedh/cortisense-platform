using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class DeleteCareTeamCommandFromRouteAssembler
{
    public static DeleteCareTeamCommand ToCommandFromRoute(int id)
    {
        return new DeleteCareTeamCommand(id);
    }
}
