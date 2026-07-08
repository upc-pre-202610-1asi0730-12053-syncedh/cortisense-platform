namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

/// <summary>
/// Represents a query to get shift records by organization id in the CortiSense Platform.
/// </summary>
public record GetShiftRecordsByOrganizationIdQuery(int OrganizationId);