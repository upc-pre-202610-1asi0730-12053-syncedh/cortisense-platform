using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

public static class UpdateClinicalAlertStatusCommandFromResourceAssembler
{
    public static UpdateClinicalAlertStatusCommand ToCommandFromResource(
        int id,
        UpdateClinicalAlertStatusResource resource)
    {
        return new UpdateClinicalAlertStatusCommand(
            id,
            resource.Status,
            resource.ResolvedBy
        );
    }
}