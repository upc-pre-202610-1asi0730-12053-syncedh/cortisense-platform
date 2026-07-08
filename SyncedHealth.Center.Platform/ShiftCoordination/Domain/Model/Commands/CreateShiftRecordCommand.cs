namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to create shift record.
/// </summary>
public record CreateShiftRecordCommand(
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