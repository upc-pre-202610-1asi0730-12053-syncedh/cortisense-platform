using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Transform;

/// <summary>
/// Represents the audit log resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class AuditLogResourceFromEntityAssembler
{
    public static AuditLogResource ToResourceFromEntity(AuditLog entity)
    {
        return new AuditLogResource(
            entity.Id,
            entity.OrganizationId,
            entity.ActorUserId,
            entity.Type.ToString(),
            entity.Severity.ToString(),
            entity.ResourceType.ToString(),
            entity.ResourceId,
            entity.Source.ToString(),
            entity.Description,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}