using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the accept invitation command from resource assembler in the CortiSense Platform.
/// </summary>
public static class AcceptInvitationCommandFromResourceAssembler
{
    public static AcceptInvitationCommand ToCommandFromResource(AcceptInvitationResource resource)
    {
        return new AcceptInvitationCommand(
            resource.Token,
            resource.FirstName,
            resource.LastName,
            resource.Password,
            resource.Phone,
            resource.WorkAreaId,
            resource.SpecialtyId
        );
    }
}