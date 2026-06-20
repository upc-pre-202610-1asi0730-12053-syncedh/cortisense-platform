namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record ShiftRecordResource(
    int Id,
    int OrganizationId,
    int UserId,
    int WorkAreaId,
    string Type,
    string Status,
    DateTimeOffset ScheduledStart,
    DateTimeOffset ScheduledEnd,
    DateTimeOffset? CheckInAt,
    DateTimeOffset? CheckOutAt
);