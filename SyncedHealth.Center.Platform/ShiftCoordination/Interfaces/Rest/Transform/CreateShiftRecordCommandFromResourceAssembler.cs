using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create shift record command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateShiftRecordCommandFromResourceAssembler
{
    public static CreateShiftRecordCommand ToCommandFromResource(CreateShiftRecordResource resource)
    {
        return new CreateShiftRecordCommand(
            resource.OrganizationId,
            resource.UserId,
            resource.WorkAreaId,
            resource.Type,
            resource.Status,
            resource.ScheduledStart,
            resource.ScheduledEnd,
            resource.CheckInAt,
            resource.CheckOutAt
        );
    }
}