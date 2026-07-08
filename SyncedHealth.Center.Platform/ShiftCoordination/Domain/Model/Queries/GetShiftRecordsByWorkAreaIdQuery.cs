namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get shift records by work area id in the CortiSense Platform.
/// </summary>
public record GetShiftRecordsByWorkAreaIdQuery(int WorkAreaId);