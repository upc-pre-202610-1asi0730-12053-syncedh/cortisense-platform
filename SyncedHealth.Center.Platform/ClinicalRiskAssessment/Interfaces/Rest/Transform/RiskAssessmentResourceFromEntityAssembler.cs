using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

/// <summary>
/// Represents the risk assessment resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class RiskAssessmentResourceFromEntityAssembler
{
    public static RiskAssessmentResource ToResourceFromEntity(RiskAssessment entity)
    {
        return new RiskAssessmentResource(
            entity.Id,
            entity.OrganizationId,
            entity.UserId,
            entity.FatigueLevel,
            entity.RiskLevel,
            entity.HeartRate,
            entity.Hrv,
            entity.LastUpdatedAt
        );
    }
}