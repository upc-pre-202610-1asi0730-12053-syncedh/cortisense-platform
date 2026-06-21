using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));

        return new AuthenticatedUserResource(
            user.Id,
            user.OrganizationId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role,
            user.Status,
            user.Phone,
            user.WorkAreaId,
            user.SpecialtyId,
            user.RegistrationStatus,
            token
        );
    }
}