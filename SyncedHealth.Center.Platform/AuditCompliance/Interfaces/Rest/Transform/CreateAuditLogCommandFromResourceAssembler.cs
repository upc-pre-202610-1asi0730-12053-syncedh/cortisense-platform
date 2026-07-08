using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;
using SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create audit log command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateAuditLogCommandFromResourceAssembler
{
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