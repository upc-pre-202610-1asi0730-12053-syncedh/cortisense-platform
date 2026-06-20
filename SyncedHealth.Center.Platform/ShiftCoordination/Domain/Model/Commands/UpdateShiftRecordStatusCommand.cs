namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

public record UpdateShiftRecordStatusCommand(
    int Id,
    string Status,
    DateTimeOffset? CheckInAt,
    DateTimeOffset? CheckOutAt
);