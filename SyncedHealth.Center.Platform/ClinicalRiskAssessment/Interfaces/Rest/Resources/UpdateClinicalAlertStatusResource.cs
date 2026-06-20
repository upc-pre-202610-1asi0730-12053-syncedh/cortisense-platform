namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

public record UpdateClinicalAlertStatusResource(
    string Status,
    int? ResolvedBy
);