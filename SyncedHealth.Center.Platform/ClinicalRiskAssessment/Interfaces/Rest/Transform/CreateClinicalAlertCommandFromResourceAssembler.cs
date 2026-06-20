using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

public static class CreateClinicalAlertCommandFromResourceAssembler
{
    public static CreateClinicalAlertCommand ToCommandFromResource(CreateClinicalAlertResource resource)
    {
        return new CreateClinicalAlertCommand(
            resource.OrganizationId,
            resource.UserId,
            resource.Severity,
            resource.Status,
            resource.Message,
            resource.CreatedAt
        );
    }
}