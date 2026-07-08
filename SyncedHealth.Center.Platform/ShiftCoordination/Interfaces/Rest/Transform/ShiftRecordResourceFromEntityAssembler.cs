using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the shift record resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class ShiftRecordResourceFromEntityAssembler
{
    public static ShiftRecordResource ToResourceFromEntity(ShiftRecord entity)
    {
        return new ShiftRecordResource(
            entity.Id,
            entity.OrganizationId,
            entity.UserId,
            entity.WorkAreaId,
            entity.Type,
            entity.Status,
            entity.ScheduledStart,
            entity.ScheduledEnd,
            entity.CheckInAt,
            entity.CheckOutAt
        );
    }
}