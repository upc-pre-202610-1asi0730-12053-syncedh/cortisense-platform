namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get team members by team id in the CortiSense Platform.
/// </summary>
public record GetTeamMembersByTeamIdQuery(int TeamId);