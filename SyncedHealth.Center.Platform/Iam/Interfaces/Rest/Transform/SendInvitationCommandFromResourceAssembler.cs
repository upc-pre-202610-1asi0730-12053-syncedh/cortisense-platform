using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the send invitation command from resource assembler in the CortiSense Platform.
/// </summary>
public static class SendInvitationCommandFromResourceAssembler
{
    public static CreateInvitationCommand ToCommandFromResource(SendInvitationResource resource)
    {
        return new CreateInvitationCommand(
            resource.OrganizationId,
            resource.Email,
            resource.Role,
            "PENDING",
            null,
            resource.ExpiresAt
        );
    }
}