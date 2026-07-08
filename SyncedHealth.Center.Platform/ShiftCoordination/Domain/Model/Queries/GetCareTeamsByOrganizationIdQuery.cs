namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get care teams by organization id in the CortiSense Platform.
/// </summary>
public record GetCareTeamsByOrganizationIdQuery(int OrganizationId);