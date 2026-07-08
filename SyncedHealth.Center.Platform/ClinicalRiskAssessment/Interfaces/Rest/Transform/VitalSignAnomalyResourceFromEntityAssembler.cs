using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

/// <summary>
/// Represents the vital sign anomaly resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class VitalSignAnomalyResourceFromEntityAssembler
{
    public static VitalSignAnomalyResource ToResourceFromEntity(VitalSignAnomaly entity)
    {
        return new VitalSignAnomalyResource(
            entity.Id,
            entity.OrganizationId,
            entity.UserId,
            entity.Type,
            entity.Severity,
            entity.Status,
            entity.Value,
            entity.Threshold,
            entity.Message,
            entity.DetectedAt,
            entity.ReviewedAt,
            entity.ReviewedBy
        );
    }
}