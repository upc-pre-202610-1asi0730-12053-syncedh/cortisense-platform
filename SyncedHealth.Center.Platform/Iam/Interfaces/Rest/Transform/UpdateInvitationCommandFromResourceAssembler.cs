using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

public static class UpdateInvitationCommandFromResourceAssembler
{
    public static UpdateInvitationCommand ToCommandFromResource(
        int id,
        UpdateInvitationResource resource)
    {
        return new UpdateInvitationCommand(
            id,
            resource.Email,
            resource.Role,
            resource.Status,
            resource.EmailStatus,
            resource.ResendEmailId,
            resource.EmailError,
            resource.ExpiresAt
        );
    }
}