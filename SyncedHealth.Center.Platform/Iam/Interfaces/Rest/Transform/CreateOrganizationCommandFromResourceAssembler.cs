using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create organization command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateOrganizationCommandFromResourceAssembler
{
    public static CreateOrganizationCommand ToCommandFromResource(CreateOrganizationResource resource)
    {
        return new CreateOrganizationCommand(
            resource.Name,
            resource.Ruc,
            resource.Email,
            resource.Phone,
            resource.Address,
            resource.Status,
            resource.RegistrationStatus
        );
    }
}