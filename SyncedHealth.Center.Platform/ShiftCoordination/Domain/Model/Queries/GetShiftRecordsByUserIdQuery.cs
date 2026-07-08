namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get shift records by user id in the CortiSense Platform.
/// </summary>
public record GetShiftRecordsByUserIdQuery(int UserId);