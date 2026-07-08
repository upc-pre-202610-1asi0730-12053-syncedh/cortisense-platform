namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to evaluate critical shift.
/// </summary>
public record EvaluateCriticalShiftCommand(int ShiftRecordId);
