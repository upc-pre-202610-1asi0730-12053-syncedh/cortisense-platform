namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to block shift.
/// </summary>
public record BlockShiftCommand(int ShiftRecordId);
