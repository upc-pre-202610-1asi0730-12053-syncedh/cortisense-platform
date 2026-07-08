using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create team member command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateTeamMemberCommandFromResourceAssembler
{
    public static CreateTeamMemberCommand ToCommandFromResource(CreateTeamMemberResource resource)
    {
        return new CreateTeamMemberCommand(
            resource.TeamId,
            resource.UserId
        );
    }
}