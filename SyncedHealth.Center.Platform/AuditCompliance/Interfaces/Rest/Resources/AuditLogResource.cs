namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to expose audit log information through the REST API.
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