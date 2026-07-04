using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class CreateWorkAreaCommandFromResourceAssembler
{
    public static CreateWorkAreaCommand ToCommandFromResource(CreateWorkAreaResource resource)
    {
        return new CreateWorkAreaCommand(
            resource.Name
        );
    }
}