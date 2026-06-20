using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;
using SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Transform;

/// <summary>
/// Assembler used to convert a create audit log resource into a command.
/// </summary>
public static class CreateAuditLogCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a create audit log resource into a create audit log command.
    /// </summary>
    /// <param name="resource">The create audit log resource.</param>
    /// <returns>The create audit log command.</returns>
    public static CreateAuditLogCommand ToCommandFromResource(CreateAuditLogResource resource)
    {
        return new CreateAuditLogCommand(
            resource.OrganizationId,
            resource.ActorUserId,
            resource.Type,
            resource.Severity,
            resource.ResourceType,
            resource.ResourceId,
            resource.Source,
            resource.Description
        );
    }
}