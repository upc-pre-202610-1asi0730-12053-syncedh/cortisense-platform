using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the care team resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class CareTeamResourceFromEntityAssembler
{
    public static CareTeamResource ToResourceFromEntity(CareTeam entity)
    {
        return new CareTeamResource
        {
            Id = entity.Id,
            OrganizationId = entity.OrganizationId,
            Name = entity.Name,
            WorkAreaId = entity.WorkAreaId,
            SupervisorId = entity.SupervisorId,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}