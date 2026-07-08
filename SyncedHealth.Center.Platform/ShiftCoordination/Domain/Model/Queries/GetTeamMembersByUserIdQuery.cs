namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get team members by user id in the CortiSense Platform.
/// </summary>
public record GetTeamMembersByUserIdQuery(int UserId);