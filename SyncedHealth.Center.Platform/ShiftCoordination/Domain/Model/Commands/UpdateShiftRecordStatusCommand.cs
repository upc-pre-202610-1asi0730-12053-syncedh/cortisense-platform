namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to update shift record status.
/// </summary>
public record UpdateShiftRecordStatusCommand(
    int Id,
    string Status,
    DateTimeOffset? CheckInAt,
    DateTimeOffset? CheckOutAt
);