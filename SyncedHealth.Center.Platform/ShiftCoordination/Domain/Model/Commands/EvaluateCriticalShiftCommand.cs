namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to evaluate if a scheduled shift represents a danger due to the holder's fatigue (US-20).
/// </summary>
public record EvaluateCriticalShiftCommand(int ShiftRecordId);
