namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

public record CheckInShiftRecordCommand(int Id, DateTimeOffset CheckInAt);
