using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Transform;

public static class RecoveryPlanResourceFromEntityAssembler
{
    public static RecoveryPlanResource ToResourceFromEntity(RecoveryPlan entity)
    {
        return new RecoveryPlanResource(
            entity.Id, 
            entity.MedicalStaffId, 
            entity.Description, 
            entity.SuggestedRestDays, 
            entity.Status);
    }
}