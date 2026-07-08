namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get care teams by work area id in the CortiSense Platform.
/// </summary>
public record GetCareTeamsByWorkAreaIdQuery(int WorkAreaId);