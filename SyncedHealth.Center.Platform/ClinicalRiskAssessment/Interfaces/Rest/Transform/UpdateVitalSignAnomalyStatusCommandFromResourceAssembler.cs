using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

/// <summary>
/// Represents the update vital sign anomaly status command from resource assembler in the CortiSense Platform.
/// </summary>
public static class UpdateVitalSignAnomalyStatusCommandFromResourceAssembler
{
    public static UpdateVitalSignAnomalyStatusCommand ToCommandFromResource(
        int id,
        UpdateVitalSignAnomalyStatusResource resource)
    {
        return new UpdateVitalSignAnomalyStatusCommand(
            id,
            resource.Status,
            resource.ReviewedBy
        );
    }
}