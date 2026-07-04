namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to block a shift preventively (US-21).
/// </summary>
public record BlockShiftCommand(int ShiftRecordId);
