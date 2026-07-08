using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create specialty command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateSpecialtyCommandFromResourceAssembler
{
    public static CreateSpecialtyCommand ToCommandFromResource(CreateSpecialtyResource resource)
    {
        return new CreateSpecialtyCommand(
            resource.Name
        );
    }
}