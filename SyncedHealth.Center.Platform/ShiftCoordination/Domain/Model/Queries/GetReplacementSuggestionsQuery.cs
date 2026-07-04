namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Query to find available staff in a work area who can cover a shift (replacement suggestions).
/// Returns shift records in SCHEDULED status within the same work area.
/// </summary>
public record GetReplacementSuggestionsQuery(int WorkAreaId, int OrganizationId);
