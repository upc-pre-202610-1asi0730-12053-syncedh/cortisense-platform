using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Transform;

/// <summary>
/// Represents the recovery plan resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class RecoveryPlanResourceFromEntityAssembler
{
    public static RecoveryPlanResource ToResourceFromEntity(RecoveryPlan entity)
    {
        return new RecoveryPlanResource(
            entity.Id,
            entity.MedicalStaffId,
            entity.Description,
            entity.SuggestedRestDays,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    public static IEnumerable<RecoveryPlanResource> ToResourceFromEntityCollection(
        IEnumerable<RecoveryPlan> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}