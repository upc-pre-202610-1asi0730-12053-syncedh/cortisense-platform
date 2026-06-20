namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;

/// <summary>
/// Query used to request audit logs by organization identifier.
/// </summary>
public record GetAuditLogsByOrganizationIdQuery(int OrganizationId);