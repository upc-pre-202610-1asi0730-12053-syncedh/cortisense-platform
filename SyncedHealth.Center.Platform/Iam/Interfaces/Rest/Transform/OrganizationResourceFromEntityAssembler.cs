using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the organization resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class OrganizationResourceFromEntityAssembler
{
    public static OrganizationResource ToResourceFromEntity(Organization entity)
    {
        return new OrganizationResource(
            entity.Id,
            entity.Name,
            entity.Ruc,
            entity.Email,
            entity.Phone,
            entity.Address,
            entity.Status,
            entity.RegistrationStatus,
            entity.ActivatedAt,
            entity.CancelledAt,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}