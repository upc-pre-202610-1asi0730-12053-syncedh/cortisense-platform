using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the update user command from resource assembler in the CortiSense Platform.
/// </summary>
public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(int id, UpdateUserResource resource)
    {
        return new UpdateUserCommand(
            id,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Password,
            resource.Phone,
            resource.WorkAreaId,
            resource.SpecialtyId,
            resource.Role,
            resource.Status,
            resource.RegistrationStatus
        );
    }
}