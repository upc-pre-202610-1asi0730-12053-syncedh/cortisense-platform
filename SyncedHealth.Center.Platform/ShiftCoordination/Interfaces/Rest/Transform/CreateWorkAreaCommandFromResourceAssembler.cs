using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create work area command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateWorkAreaCommandFromResourceAssembler
{
    public static CreateWorkAreaCommand ToCommandFromResource(CreateWorkAreaResource resource)
    {
        return new CreateWorkAreaCommand(
            resource.Name
        );
    }
}