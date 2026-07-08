namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to reassign shift.
/// </summary>
public record ReassignShiftCommand(int ShiftRecordId, int NewUserId);
