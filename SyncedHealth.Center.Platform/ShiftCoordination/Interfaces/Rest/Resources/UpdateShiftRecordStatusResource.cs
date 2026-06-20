namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record UpdateShiftRecordStatusResource(
    string Status,
    DateTimeOffset? CheckInAt,
    DateTimeOffset? CheckOutAt
);