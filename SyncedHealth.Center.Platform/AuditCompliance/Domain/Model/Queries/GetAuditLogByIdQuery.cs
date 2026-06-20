namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;

/// <summary>
/// Query used to request an audit log by its identifier.
/// </summary>
public record GetAuditLogByIdQuery(int AuditLogId);