using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

public static class InvitationResourceFromEntityAssembler
{
    public static InvitationResource ToResourceFromEntity(Invitation entity)
    {
        return new InvitationResource(
            entity.Id,
            entity.OrganizationId,
            entity.Email,
            entity.Role,
            entity.Status,
            entity.Token,
            entity.EmailStatus,
            entity.ResendEmailId,
            entity.EmailError,
            entity.ExpiresAt,
            entity.SentAt,
            entity.AcceptedAt,
            entity.CancelledAt,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}