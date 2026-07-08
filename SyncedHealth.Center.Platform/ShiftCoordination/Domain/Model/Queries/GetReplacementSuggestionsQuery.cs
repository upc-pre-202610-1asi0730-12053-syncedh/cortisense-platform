namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get replacement suggestions in the CortiSense Platform.
/// </summary>
public record GetReplacementSuggestionsQuery(int WorkAreaId, int OrganizationId);
