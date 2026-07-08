using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;

/// <summary>
/// Command to create audit log.
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