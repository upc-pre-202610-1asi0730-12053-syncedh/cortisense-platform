namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

/// <summary>
/// Represents the audit log resource in the CortiSense Platform.
/// </summary>
public record AuditLogResource(
    int Id,
    int OrganizationId,
    int ActorUserId,
    string Type,
    string Severity,
    string ResourceType,
    int ResourceId,
    string Source,
    string Description,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);