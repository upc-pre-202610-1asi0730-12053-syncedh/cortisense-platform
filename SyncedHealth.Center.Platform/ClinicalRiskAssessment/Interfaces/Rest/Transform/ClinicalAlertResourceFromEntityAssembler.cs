using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

/// <summary>
/// Represents the clinical alert resource from entity assembler in the CortiSense Platform.
/// </summary>
public static class ClinicalAlertResourceFromEntityAssembler
{
    public static ClinicalAlertResource ToResourceFromEntity(ClinicalAlert entity)
    {
        return new ClinicalAlertResource(
            entity.Id,
            entity.OrganizationId,
            entity.UserId,
            entity.Severity,
            entity.Status,
            entity.Message,
            entity.CreatedAt,
            entity.ResolvedAt,
            entity.ResolvedBy
        );
    }
}