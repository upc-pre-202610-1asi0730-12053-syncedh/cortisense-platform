using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Transform;

/// <summary>
/// Assembler used to convert an audit log entity into a REST resource.
/// </summary>
public static class AuditLogResourceFromEntityAssembler
{
    /// <summary>
    /// Converts an audit log entity into an audit log resource.
    /// </summary>
    /// <param name="entity">The audit log entity.</param>
    /// <returns>The audit log resource.</returns>
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