using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class SpecialtyResourceFromEntityAssembler
{
    public static SpecialtyResource ToResourceFromEntity(Specialty entity)
    {
        return new SpecialtyResource
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}