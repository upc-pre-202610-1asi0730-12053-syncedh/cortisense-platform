using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
/// Represents the assign role command from resource assembler in the CortiSense Platform.
/// </summary>
public static class AssignRoleCommandFromResourceAssembler
{
    public static AssignRoleCommand ToCommandFromResource(int userId, AssignRoleResource resource)
    {
        return new AssignRoleCommand(userId, resource.Role);
    }
}
