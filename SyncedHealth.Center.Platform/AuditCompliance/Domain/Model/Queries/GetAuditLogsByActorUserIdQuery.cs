namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;

/// <summary>
/// Represents a query to get audit logs by actor user id in the CortiSense Platform.
/// </summary>
public record GetAuditLogsByActorUserIdQuery(int ActorUserId);