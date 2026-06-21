using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class TeamMemberResourceFromEntityAssembler
{
    public static TeamMemberResource ToResourceFromEntity(TeamMember entity)
    {
        return new TeamMemberResource
        {
            Id = entity.Id,
            TeamId = entity.TeamId,
            UserId = entity.UserId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}