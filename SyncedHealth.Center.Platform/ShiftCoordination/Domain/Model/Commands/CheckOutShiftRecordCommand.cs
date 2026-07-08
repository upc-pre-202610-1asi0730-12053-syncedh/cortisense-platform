namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to check out shift record.
/// </summary>
public record CheckOutShiftRecordCommand(int Id, DateTimeOffset CheckOutAt);
