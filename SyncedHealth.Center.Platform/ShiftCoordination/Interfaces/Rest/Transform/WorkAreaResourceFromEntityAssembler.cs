using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

/// <summary>
/// Represents the work area resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class WorkAreaResourceFromEntityAssembler
{
    public static WorkAreaResource ToResourceFromEntity(WorkArea entity)
    {
        return new WorkAreaResource
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}