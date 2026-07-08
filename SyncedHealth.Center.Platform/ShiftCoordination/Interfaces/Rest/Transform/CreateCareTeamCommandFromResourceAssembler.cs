using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create care team command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateCareTeamCommandFromResourceAssembler
{
    public static CreateCareTeamCommand ToCommandFromResource(CreateCareTeamResource resource)
    {
        return new CreateCareTeamCommand(
            resource.OrganizationId,
            resource.Name,
            resource.WorkAreaId,
            resource.SupervisorId ?? 0,
            resource.Status
        );
    }
}