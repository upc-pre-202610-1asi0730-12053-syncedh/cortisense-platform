using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;

/// <summary>
/// Command used to request the creation of an audit log entry.
/// </summary>
public record CreateAuditLogCommand(
    int OrganizationId,
    int ActorUserId,
    EAuditLogType Type,
    EAuditSeverity Severity,
    EAuditResourceType ResourceType,
    int ResourceId,
    EAuditActionSource Source,
    string Description
);