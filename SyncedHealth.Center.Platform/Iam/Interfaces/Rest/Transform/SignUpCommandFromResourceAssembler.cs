using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.OrganizationId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Password,
            resource.Role,
            resource.Status,
            resource.Phone,
            resource.WorkAreaId,
            resource.SpecialtyId,
            resource.RegistrationStatus
        );
    }
}