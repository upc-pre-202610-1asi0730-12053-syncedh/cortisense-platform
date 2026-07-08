namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create shift record resource in the CortiSense Platform.
/// </summary>
public record CreateShiftRecordResource(
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