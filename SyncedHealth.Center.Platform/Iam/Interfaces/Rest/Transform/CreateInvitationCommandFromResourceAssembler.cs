using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create invitation command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateInvitationCommandFromResourceAssembler
{
    public static CreateInvitationCommand ToCommandFromResource(CreateInvitationResource resource)
    {
        return new CreateInvitationCommand(
            resource.OrganizationId,
            resource.Email,
            resource.Role,
            resource.Status,
            resource.Token,
            resource.ExpiresAt
        );
    }
}