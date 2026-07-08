using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the update organization command from resource assembler in the CortiSense Platform.
/// </summary>
public static class UpdateOrganizationCommandFromResourceAssembler
{
    public static UpdateOrganizationCommand ToCommandFromResource(
        int id,
        UpdateOrganizationResource resource)
    {
        return new UpdateOrganizationCommand(
            id,
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