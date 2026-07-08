namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the update shift record status resource in the CortiSense Platform.
/// </summary>
public record UpdateShiftRecordStatusResource(
    string Status,
    DateTimeOffset? CheckInAt,
    DateTimeOffset? CheckOutAt
);