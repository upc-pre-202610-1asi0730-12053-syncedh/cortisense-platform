using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class CreateSpecialtyCommandFromResourceAssembler
{
    public static CreateSpecialtyCommand ToCommandFromResource(CreateSpecialtyResource resource)
    {
        return new CreateSpecialtyCommand(
            resource.Name
        );
    }
}