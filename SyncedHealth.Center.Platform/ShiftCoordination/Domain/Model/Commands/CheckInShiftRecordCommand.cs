namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to check in shift record.
/// </summary>
public record CheckInShiftRecordCommand(int Id, DateTimeOffset CheckInAt);
