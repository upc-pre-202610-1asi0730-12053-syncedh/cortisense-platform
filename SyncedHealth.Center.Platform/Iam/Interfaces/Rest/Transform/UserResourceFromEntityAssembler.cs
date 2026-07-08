using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the user resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            entity.Id,
            entity.OrganizationId,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.Role,
            entity.Status,
            entity.Phone,
            entity.WorkAreaId,
            entity.SpecialtyId,
            entity.RegistrationStatus,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}