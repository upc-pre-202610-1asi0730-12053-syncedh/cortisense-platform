namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

public record CheckOutShiftRecordCommand(int Id, DateTimeOffset CheckOutAt);
