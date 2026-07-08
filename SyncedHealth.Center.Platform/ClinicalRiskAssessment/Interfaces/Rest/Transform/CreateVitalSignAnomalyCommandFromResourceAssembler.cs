using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create vital sign anomaly command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateVitalSignAnomalyCommandFromResourceAssembler
{
    public static CreateVitalSignAnomalyCommand ToCommandFromResource(CreateVitalSignAnomalyResource resource)
    {
        return new CreateVitalSignAnomalyCommand(
            resource.OrganizationId,
            resource.UserId,
            resource.Type,
            resource.Severity,
            resource.Status,
            resource.Value,
            resource.Threshold,
            resource.Message,
            resource.DetectedAt
        );
    }
}