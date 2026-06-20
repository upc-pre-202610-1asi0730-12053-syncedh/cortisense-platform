using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

public static class VitalSignReadingResourceFromEntityAssembler
{
    public static VitalSignReadingResource ToResourceFromEntity(VitalSignReading entity)
    {
        return new VitalSignReadingResource(
            entity.Id,
            entity.OrganizationId,
            entity.UserId,
            entity.HeartRate,
            entity.Hrv,
            entity.FatigueLevel,
            entity.CortisolLevel,
            entity.SensorStatus,
            entity.RecordedAt
        );
    }
}