namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;

/// <summary>
/// Represents a query to get audit log by id in the CortiSense Platform.
/// </summary>
public record GetAuditLogByIdQuery(int AuditLogId);