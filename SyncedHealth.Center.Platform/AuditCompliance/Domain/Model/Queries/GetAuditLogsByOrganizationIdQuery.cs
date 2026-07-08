namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;

/// <summary>
/// Represents a query to get audit logs by organization id in the CortiSense Platform.
/// </summary>
public record GetAuditLogsByOrganizationIdQuery(int OrganizationId);