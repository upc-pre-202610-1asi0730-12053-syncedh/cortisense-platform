using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the update shift record status command from resource assembler in the CortiSense Platform.
/// </summary>
public static class UpdateShiftRecordStatusCommandFromResourceAssembler
{
    public static UpdateShiftRecordStatusCommand ToCommandFromResource(
        int id,
        UpdateShiftRecordStatusResource resource)
    {
        return new UpdateShiftRecordStatusCommand(
            id,
            resource.Status,
            resource.CheckInAt,
            resource.CheckOutAt
        );
    }
}