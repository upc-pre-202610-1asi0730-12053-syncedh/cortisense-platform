using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;

public static class AssignRoleCommandFromResourceAssembler
{
    public static AssignRoleCommand ToCommandFromResource(int userId, AssignRoleResource resource)
    {
        return new AssignRoleCommand(userId, resource.Role);
    }
}
