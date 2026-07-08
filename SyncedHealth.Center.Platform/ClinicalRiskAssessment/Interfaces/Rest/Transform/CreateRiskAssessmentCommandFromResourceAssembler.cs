using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create risk assessment command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateRiskAssessmentCommandFromResourceAssembler
{
    public static CreateRiskAssessmentCommand ToCommandFromResource(CreateRiskAssessmentResource resource)
    {
        return new CreateRiskAssessmentCommand(
            resource.OrganizationId,
            resource.UserId,
            resource.FatigueLevel,
            resource.RiskLevel,
            resource.HeartRate,
            resource.Hrv,
            resource.LastUpdatedAt
        );
    }
}