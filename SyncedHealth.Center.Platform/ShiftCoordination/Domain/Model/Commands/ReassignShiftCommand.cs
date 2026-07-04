namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to reassign a blocked shift to a suggested replacement (US-23).
/// </summary>
public record ReassignShiftCommand(int ShiftRecordId, int NewUserId);
