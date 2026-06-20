using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

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