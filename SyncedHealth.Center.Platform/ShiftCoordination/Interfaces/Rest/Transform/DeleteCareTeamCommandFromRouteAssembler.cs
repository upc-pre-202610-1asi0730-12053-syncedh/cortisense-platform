using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the delete care team command from route assembler in the CortiSense Platform.
/// </summary>
public static class DeleteCareTeamCommandFromRouteAssembler
{
    public static DeleteCareTeamCommand ToCommandFromRoute(int id)
    {
        return new DeleteCareTeamCommand(id);
    }
}
